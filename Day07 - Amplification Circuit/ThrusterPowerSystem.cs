using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019.Day07;

public class ThrusterPowerSystem
{
	private int StartPower { get; }
	private IList<IntcodeMachine> Amplifiers { get; }

	public ThrusterPowerSystem(int[] ampProgram, int nofAmplifiers, int startPower)
	{
		Amplifiers = Enumerable.Range(0, nofAmplifiers).Select(_ => new IntcodeMachine(ampProgram)).ToArray();
		StartPower = startPower;
	}

	public void Reset()
	{
		foreach (IntcodeMachine amplifier in Amplifiers)
		{
			amplifier.ResetMachine();
		}
	}

	public int GetThrusterPower(IReadOnlyList<int> phaseSettings)
	{
		if (phaseSettings is null)
		{
			throw new ArgumentNullException(nameof(phaseSettings));
		}
		if (phaseSettings.Count != Amplifiers.Count)
		{
			throw new ArgumentException("The length of the phase settings array should match the number of amplifiers.");
		}
		int power = 0;
		for (int i = 0; i < Amplifiers.Count; i++)
		{
			Amplifiers[i].ResetMachine();
			Amplifiers[i].InputValues = new List<int> { phaseSettings[i], power };
			Amplifiers[i].OutputStream = new MemoryStream();
			_ = Amplifiers[i].Run();
			_ = Amplifiers[i].OutputStream.Seek(0, SeekOrigin.Begin);
			using StreamReader reader = new(Amplifiers[i].OutputStream);
			power = int.Parse(reader.ReadToEnd());
		}
		return power;
	}

	public int GetRecurrentThrusterPower(IReadOnlyList<int> phaseSettings, bool resetAtStart = false)
	{
		if (phaseSettings is null)
		{
			throw new ArgumentNullException(nameof(phaseSettings));
		}
		if (phaseSettings.Count != Amplifiers.Count)
		{
			throw new ArgumentException("The length of the phase settings array should match the number of amplifiers.");
		}
		if (resetAtStart)
		{
			Reset();
		}
		int[] lastSignals = new int[Amplifiers.Count];
		for (int i = 0; i < Amplifiers.Count; i++)
		{
			Amplifiers[i].InputValues = new List<int> { phaseSettings[i] };
		}
		int power = StartPower;
		for (int i = 0; ; i = (i + 1) % Amplifiers.Count)
		{
			Amplifiers[i].InputValues.Add(power);
			Amplifiers[i].OutputStream = new MemoryStream();
			bool finished = Amplifiers[i].Run(breakOnOutputWritten: true);
			if (finished)
			{
				return lastSignals[^1];
			}
			_ = Amplifiers[i].OutputStream.Seek(0, SeekOrigin.Begin);
			using StreamReader reader = new(Amplifiers[i].OutputStream);
			power = int.Parse(reader.ReadToEnd());
			lastSignals[i] = power;
		}
	}
}
