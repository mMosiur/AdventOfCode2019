namespace AdventOfCode.Year2019.Day23;

public class IntcodeNetwork
{
	private const int NAT_ADDRESS = 255;
	public const int BASE_DELAY = 10;

	private readonly Dictionary<long, INetworkDevice> _devices = new();
	private readonly Dictionary<INetworkDevice, Task> _processes = new();
	private readonly CancellationTokenSource _tokenSource = new();
	private readonly bool _verbose;

	public NAT _nat { get; }

	public IntcodeNetwork(long[] program, int machineCount, bool verbose = true)
	{
		var machines = new IntcodeMachineWithNetworkCard[machineCount];
		for (int i = 0; i < machineCount; i++)
		{
			machines[i] = new IntcodeMachineWithNetworkCard(program, address: i);
			AddNetworkDevice(i, machines[i]);
		}
		_nat = new NAT(machines, address: NAT_ADDRESS, verbose);
		AddNetworkDevice(NAT_ADDRESS, _nat);
		_verbose = verbose;
	}

	private void OnMessageSent(object? sender, MessageSentEventArgs e)
	{
		if (_verbose)
		{
			Console.WriteLine($"({e.SenderAddress} -> {e.RecipientAddress}): {e.Packet.X} {e.Packet.Y}");
		}
		_devices[e.RecipientAddress].ReceivePacket(e.Packet);
	}

	public Task Run() => Task.Run(() =>
	{
		foreach (var device in _devices.Values)
		{
			if (!_processes.TryGetValue(device, out Task? process))
			{
				process = device.Start(_tokenSource.Token);
				_processes.Add(device, process);
			}
		}
		var processesArray = _processes.Values.ToArray();
		Task.WaitAny(processesArray);
		_tokenSource.Cancel();
		Task.WaitAll(processesArray);
	});

	public long AddNetworkDevice(long address, INetworkDevice device)
	{
		_devices.Add(address, device);
		device.MessageSent += OnMessageSent;
		return address;
	}

	public long GetFirstNatReceivedY() => Task.Run(async () =>
	{
		while (_nat.FirstReceivedY is null)
		{
			await Task.Delay(BASE_DELAY);
		}
		return _nat.FirstReceivedY.Value;
	}).Result;

	public long GetLastNatSentY() => Task.Run(async () =>
	{
		while (_nat.LastDeliveredY is null)
		{
			await Task.Delay(BASE_DELAY);
		}
		return _nat.LastDeliveredY.Value;
	}).Result;

	public void RemoveNetworkDevice(long address)
	{
		if (!_devices.TryGetValue(address, out INetworkDevice? device))
		{
			throw new InvalidOperationException("No device with that address");
		}
		device.MessageSent -= OnMessageSent;
		_devices.Remove(address);
	}
}
