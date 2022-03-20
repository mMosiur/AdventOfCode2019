namespace AdventOfCode.Year2019.Day24;

public interface IBugGridArea
{
	int Minute { get; }
	int BugCount { get; }
	void NextMinute();
}
