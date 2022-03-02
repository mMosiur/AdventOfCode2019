using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day04;

public class Day04Solver : DaySolver
{
	private int _start;
	private int _end;
	private PasswordChecker _passwordChecker;

	public Day04Solver(string inputFilePath) : base(inputFilePath)
	{
		(_start, _end) = Input.Trim().Split('-', StringSplitOptions.TrimEntries).Select(int.Parse).SplitIntoTwo();
		_passwordChecker = new(_start, _end);
	}

	public override string SolvePart1()
	{
		int part1 = _passwordChecker
			.AllMatchingPassword(strictAdjacentRule: false)
			.Count();
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		int part2 = _passwordChecker
			.AllMatchingPassword(strictAdjacentRule: true)
			.Count();
		return part2.ToString();
	}
}
