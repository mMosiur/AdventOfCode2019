namespace AdventOfCode.Year2019.Day22;

public class SimpleCardShuffler : ICardShuffler
{
	private List<int> _deck;

	public SimpleCardShuffler(int numberOfCards)
	{
		if (numberOfCards <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(numberOfCards), "Number of cards must be positive.");
		}
		_deck = Enumerable.Range(0, numberOfCards).ToList();
	}

	public void DealIntoNewStack()
	{
		_deck.Reverse();
	}

	public void CutN(int n)
	{
		if (n > 0)
		{
			List<int> cut = _deck.GetRange(0, n);
			_deck.RemoveRange(0, n);
			_deck.AddRange(cut);
		}
		else if (n < 0)
		{
			n = -n;
			List<int> cut = _deck.GetRange(_deck.Count - n, n);
			_deck.RemoveRange(_deck.Count - n, n);
			_deck.InsertRange(0, cut);
		}
	}

	public void DealWithIncrementN(int n)
	{
		List<int> newDeck = Enumerable.Repeat(-1, _deck.Count).ToList();
		int index = 0;
		foreach (int card in _deck)
		{
			if (newDeck[index] >= 0)
			{
				throw new InvalidOperationException("Deck is not properly shuffled.");
			}
			newDeck[index] = card;
			index = (index + n) % newDeck.Count;
		}
		if (newDeck.Skip(1).Any(c => c < 0))
		{
			throw new InvalidOperationException("Deck is not properly shuffled.");
		}
		_deck = newDeck.ToList();
	}

	public long GetCardIndex(int cardNumber)
	{
		int index = _deck.IndexOf(cardNumber);
		if (index < 0)
		{
			throw new ArgumentException($"Card {cardNumber} not found in deck.", nameof(cardNumber));
		}
		return index;
	}
}
