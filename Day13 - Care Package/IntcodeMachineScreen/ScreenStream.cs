using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019.Day13;

public class ScreenStream : Stream
{
	private readonly List<byte> _binaryBuffer;
	private readonly List<long> _numericBuffer;

	private IEnumerable<string> ReadLines()
	{
		string line = "";
		foreach (byte b in _binaryBuffer)
		{
			line += (char)b;
			if (line.EndsWith(Environment.NewLine))
			{
				yield return line.Remove(line.Length - Environment.NewLine.Length);
				line = "";
			}
		}
		if (!string.IsNullOrEmpty(line))
		{
			yield return line;
		}
	}

	private IEnumerable<(long X, long Y, long TileId)> ReadTiles()
	{
		for (int i = 0; i + 3 <= _numericBuffer.Count; i += 3)
		{
			yield return (
				_numericBuffer[i + 0],
				_numericBuffer[i + 1],
				_numericBuffer[i + 2]
			);
		}
	}

	public char[,] Frame { get; private set; }

	public long Score { get; private set; }

	public ScreenStream() : this(0, 0) { }

	public ScreenStream(long initialScreenWidth, long initialScreenHeight)
	{
		_binaryBuffer = new();
		_numericBuffer = new();
		Score = 0;
		Frame = new char[initialScreenWidth, initialScreenHeight];
	}

	public void Clear()
	{
		_binaryBuffer.Clear();
		Score = 0;
		Frame = new char[0, 0];
	}

	public override bool CanRead => false;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Length => _binaryBuffer.Count;

	public override long Position
	{
		get => _binaryBuffer.Count;
		set => throw new NotSupportedException();
	}

	public override void Flush()
	{
		_numericBuffer.AddRange(ReadLines().Select(long.Parse));
		_binaryBuffer.Clear();
		(long X, long Y, long TileId)[] arr = ReadTiles().ToArray();
		if (arr.Length == 0)
		{
			return;
		}
		long maxX = arr.Max(v => v.X);
		long maxY = arr.Max(v => v.Y);
		if (maxX >= Frame.GetLongLength(0) || maxY >= Frame.GetLongLength(1))
		{
			long xNewLength = Math.Max(maxX + 1, Frame.GetLongLength(0));
			long yNewLength = Math.Max(maxY + 1, Frame.GetLongLength(1));
			char[,] newFrame = new char[xNewLength, yNewLength];
			for (long x = 0; x < Frame.GetLongLength(0); x++)
			{
				for (long y = 0; y < Frame.GetLongLength(1); y++)
				{
					newFrame[x, y] = Frame[x, y];
				}
			}
			Frame = newFrame;
		}
		foreach (var (x, y, id) in arr)
		{
			if (x == -1 && y == 0)
			{
				Score = id;
			}
			else
			{
				Frame[x, y] = ((Tile)id).ToChar();
			}
		}
		int readValues = arr.Length * 3;
		_numericBuffer.RemoveRange(0, readValues);
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException();
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
		_binaryBuffer.AddRange(buffer.Skip(offset).Take(count));
	}
}
