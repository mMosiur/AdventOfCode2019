using System;
using System.IO;
using System.Text;

namespace Day13;

public class IntcodeMachineScreen : IDisposable
{
	private readonly ScreenStream _screen;
	private Stream _previousStream;
	public IntcodeMachine? ConnectedMachine { get; private set; }

	public long Score => _screen.Score;

	public char[,] Frame => _screen.Frame;

	public bool IsConnected => ConnectedMachine is not null;

	public IntcodeMachineScreen() : this(0, 0) { }

	public IntcodeMachineScreen(long initialScreenWidth, long initialScreenHeight)
	{
		_screen = new ScreenStream(initialScreenWidth, initialScreenHeight);
		_previousStream = Stream.Null;
		ConnectedMachine = null;
	}

	public void ConnectTo(IntcodeMachine machine)
	{
		if (ConnectedMachine is not null)
		{
			if (ReferenceEquals(ConnectedMachine, machine))
			{
				return;
			}
			else
			{
				throw new InvalidOperationException("Screen is already connected to another machine");
			}
		}
		ConnectedMachine = machine;
		_previousStream = ConnectedMachine.OutputStream;
		ConnectedMachine.OutputStream = _screen;
	}

	public void Disconnect()
	{
		if (ConnectedMachine is null)
		{
			return;
		}
		if (ReferenceEquals(ConnectedMachine.OutputStream, _screen))
		{
			ConnectedMachine.OutputStream = _previousStream;
			_screen.Clear();
		}
		ConnectedMachine = null;
	}

	public void Dispose()
	{
		_screen.Dispose();
		GC.SuppressFinalize(this);
	}

	public override string ToString()
	{
		StringBuilder builder = new();
		for (int r = 0; r < Frame.GetLength(1); r++)
		{
			for (int c = 0; c < Frame.GetLength(0); c++)
			{
				builder.Append(Frame[c, r]);
			}
			builder.AppendLine();
		}
		return builder.ToString();
	}
}
