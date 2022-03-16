namespace AdventOfCode.Year2019.Day23;

public interface INetworkDevice
{
	public void ReceivePacket(Packet packet);
	public event EventHandler<MessageSentEventArgs>? MessageSent;
	public Task Start(CancellationToken token);
}
