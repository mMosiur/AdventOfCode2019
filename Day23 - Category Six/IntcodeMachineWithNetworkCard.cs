namespace AdventOfCode.Year2019.Day23;

public class IntcodeMachineWithNetworkCard : INetworkDevice
{
	private readonly long _networkAddress;
	private Queue<long> _networkInputQueue = new();
	private IntcodeMachine _machine;

	public bool IsIdle { get; private set; } = false;

	public IntcodeMachineWithNetworkCard(long[] program, long address)
	{
		_networkAddress = address;
		_networkInputQueue.Enqueue(address);
		_machine = new IntcodeMachine(
			program,
			GetInput
		);
	}

	public Task Start(CancellationToken token) => Task.Run(() =>
	{
		List<long> message = new List<long>(3);
		try
		{
			foreach (long output in _machine.RunYieldingOutput(token))
			{
				IsIdle = false;
				message.Add(output);
				if (message.Count == 3)
				{
					OnMessageSent(message[0], message[1], message[2]);
					message.Clear();
				}
			}
		}
		catch (OperationCanceledException) { }
	});

	public event EventHandler<MessageSentEventArgs>? MessageSent;

	protected virtual void OnMessageSent(MessageSentEventArgs e) => MessageSent?.Invoke(this, e);

	protected void OnMessageSent(long address, long x, long y) => OnMessageSent(new MessageSentEventArgs(_networkAddress, address, x, y));

	private long GetInput() => GetInputAsync().Result;

	private async Task<long> GetInputAsync()
	{
		if (!_networkInputQueue.TryDequeue(out long input))
		{
			input = -1;
			await Task.Delay(IntcodeNetwork.BASE_DELAY);
			IsIdle = true;
		}
		else
		{
			IsIdle = false;
		}
		return input;
	}

	public void ReceivePacket(Packet packet)
	{
		_networkInputQueue.Enqueue(packet.X);
		_networkInputQueue.Enqueue(packet.Y);
	}
}
