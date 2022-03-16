namespace AdventOfCode.Year2019.Day23;

public class MessageSentEventArgs : EventArgs
{
	public long SenderAddress { get; }
	public long RecipientAddress { get; }
	public Packet Packet { get; }

	public MessageSentEventArgs(long senderAddress, long recipientAddress, Packet packet)
	{
		SenderAddress = senderAddress;
		RecipientAddress = recipientAddress;
		Packet = packet;
	}

	public MessageSentEventArgs(long senderAddress, long recipientAddress, long x, long y)
		: this(senderAddress, recipientAddress, new Packet(x, y)) { }
}
