namespace AdventOfCode.Year2019.Day23;

public class NAT : INetworkDevice
{
	private Packet? _lastPacket;

	public Packet LastPacket { get => _lastPacket!.Value; private set => _lastPacket = value; }

	private readonly ICollection<IntcodeMachineWithNetworkCard> _machines;
	private readonly long _address;
	private readonly bool _verbose;
	public long? _previousDeliveredY;
	public long? LastDeliveredY { get; private set; }
	public long? FirstReceivedY { get; private set; }

	public NAT(ICollection<IntcodeMachineWithNetworkCard> machinesToMonitor, long address, bool verbose = true)
	{
		_machines = machinesToMonitor;
		_address = address;
		_verbose = verbose;
	}

	public event EventHandler<MessageSentEventArgs>? MessageSent;

	protected virtual void OnMessageSent(MessageSentEventArgs e) => MessageSent?.Invoke(this, e);

	protected void OnMessageSent(long address, Packet packet) => OnMessageSent(new MessageSentEventArgs(_address, address, packet));

	public Task Start(CancellationToken token) => Task.Run(async () =>
	{
		try
		{
			while (!token.IsCancellationRequested)
			{
				await Task.Delay(IntcodeNetwork.BASE_DELAY, token);
				if (_machines.All(m => m.IsIdle))
				{
					await Task.Delay(IntcodeNetwork.BASE_DELAY * 2, token);
					if (!_machines.All(m => m.IsIdle)) continue; // Double check for false positive
					if (_verbose)
					{
						Console.WriteLine("Network idle");
					}
					OnMessageSent(0, LastPacket);
					if (_previousDeliveredY == LastPacket.Y)
					{
						LastDeliveredY = _previousDeliveredY;
						return;
					}
					_previousDeliveredY = LastPacket.Y;
					await Task.Delay(IntcodeNetwork.BASE_DELAY * 2, token);
				}
			}
		}
		catch (TaskCanceledException) { }
	});

	public void ReceivePacket(Packet packet)
	{
		FirstReceivedY ??= packet.Y;
		LastPacket = packet;
	}
}
