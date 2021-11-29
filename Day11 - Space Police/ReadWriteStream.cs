using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11;

public class ReadWriteStream : Stream
{
	private readonly List<byte> _buffer = new();
	private int _readingPosition = 0;

	public override bool CanRead => true;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Length => _buffer.Count;

	public override long Position
	{
		get => _buffer.Count;
		set => throw new NotSupportedException();
	}

	public override void Flush() { }

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (buffer is null) throw new ArgumentNullException(nameof(buffer));
		if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
		if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
		if (offset + count > buffer.Length) throw new ArgumentException("The sum of offset and count is larger than the buffer length.");
		int maxCountToWrite = count;
		int maxCountToRead = _buffer.Count - _readingPosition;
		int maxCount = Math.Min(maxCountToWrite, maxCountToRead);
		_buffer.CopyTo(_readingPosition, buffer, offset, maxCount);
		_readingPosition += maxCount;
		return maxCount;
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotSupportedException();
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (buffer is null) throw new ArgumentNullException(nameof(buffer));
		if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
		if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
		if (offset + count > buffer.Length) throw new ArgumentException("The sum of offset and count is greater than the buffer length.");
		_buffer.AddRange(buffer.Skip(offset).Take(count));
	}
}
