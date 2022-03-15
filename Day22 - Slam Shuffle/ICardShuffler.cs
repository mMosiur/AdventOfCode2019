namespace AdventOfCode.Year2019.Day22;

public interface ICardShuffler
{
	void DealIntoNewStack();
	void CutN(int n);
	void DealWithIncrementN(int n);
	long GetCardIndex(int cardNumber);
}
