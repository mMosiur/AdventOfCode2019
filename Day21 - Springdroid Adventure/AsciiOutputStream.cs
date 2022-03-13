using System.Text;

namespace AdventOfCode.Year2019.Day21;

/// <summary>
/// A middleware Stream class that encapsulates a stream that writes numerical values as digits,
/// catches them and converts to ASCII characters, that then passes on to the underlying stream.
/// </summary>
public class AsciiOutputStream : Stream
{
	private readonly Stream _stream;

	public AsciiOutputStream(Stream stream)
	{
		ArgumentNullException.ThrowIfNull(stream);
		_stream = stream;
	}

	public override bool CanRead => _stream.CanRead;

	public override bool CanSeek => _stream.CanSeek;

	public override bool CanWrite => _stream.CanWrite;

	public override long Length => _stream.Length;

	public override long Position { get => _stream.Position; set => _stream.Position = value; }

	public override void Flush() => _stream.Flush();

	public override int Read(byte[] buffer, int offset, int count) => _stream.Read(buffer, offset, count);

	public override long Seek(long offset, SeekOrigin origin) => _stream.Seek(offset, origin);

	public override void SetLength(long value) => _stream.SetLength(value);

	public override void Write(byte[] buffer, int offset, int count)
	{
		string[] charsStrings = Encoding.ASCII
			.GetString(buffer, offset, count)
			.Split('\n', StringSplitOptions.RemoveEmptyEntries);
		byte[] newBuffer;
		try
		{
			newBuffer = charsStrings.Select(byte.Parse).ToArray();
		}
		catch (OverflowException)
		{
			List<byte> byteList = new();
			foreach (string charsString in charsStrings)
			{
				if (byte.TryParse(charsString, out byte b))
				{
					byteList.Add(b);
					continue;
				}
				foreach (char c in charsString)
				{
					byteList.Add((byte)c);
				}
				byteList.Add((byte)'\n');
			}
			newBuffer = byteList.ToArray();
		}
		_stream.Write(newBuffer, 0, newBuffer.Length);
	}
}
