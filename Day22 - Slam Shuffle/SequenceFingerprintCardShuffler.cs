using System.Numerics;

namespace AdventOfCode.Year2019.Day22;

public class SequenceFingerprintCardShuffler : ICardShuffler
{
	private readonly long _numberOfCards;
	private readonly ModuloCalculator _calc;

	private BigInteger _incrementMultiplier = 1;
	private BigInteger _offsetDifference = 0;

	public SequenceFingerprintCardShuffler(long numberOfCards)
	{
		_numberOfCards = numberOfCards;
		_calc = new ModuloCalculator(
			modulus: _numberOfCards,
			allowNegative: false
		);
	}

	public void DealIntoNewStack()
	{
		_incrementMultiplier = _calc.Mod(-_incrementMultiplier);
		_offsetDifference = _calc.ModAdd(_offsetDifference, _incrementMultiplier);
	}

	public void CutN(int n)
	{
		_offsetDifference = _calc.ModAdd(_offsetDifference, n * _incrementMultiplier);
	}

	public void DealWithIncrementN(int n)
	{
		_incrementMultiplier = _calc.ModMultiply(_incrementMultiplier, _calc.ModInverse(n));
	}

	public long GetCardIndex(int cardNumber, long numberOfRepetitions)
	{
		BigInteger increment = _calc.ModPower(_incrementMultiplier, numberOfRepetitions);
		BigInteger offset = _calc.ModMultiply(
			_offsetDifference,
			(1 - increment),
			_calc.ModInverse(_calc.Mod(1 - _incrementMultiplier))
		);
		long cardIndex = (long)_calc.Mod(cardNumber * increment + offset);
		return cardIndex;
	}

	public long GetCardIndex(int cardNumber) => GetCardIndex(cardNumber, 1);
}
