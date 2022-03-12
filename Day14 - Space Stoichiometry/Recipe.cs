using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day14;

public class Recipe
{
	private readonly IList<RecipeEntry> _inputs;
	private readonly RecipeEntry _output;

	public IReadOnlyList<RecipeEntry> InputChemicals => (IReadOnlyList<RecipeEntry>)_inputs;

	public RecipeEntry OutputChemical => _output;

	private Recipe(IList<RecipeEntry> inputChemicals, RecipeEntry outputChemical)
	{
		_inputs = inputChemicals;
		_output = outputChemical;
	}

	public static Recipe Parse(string s)
	{
		string[] parts = s.Split("=>", StringSplitOptions.TrimEntries);
		if (parts.Length != 2)
		{
			throw new FormatException();
		}
		return new Recipe(
			ParseInputs(parts[0]),
			ParseOutput(parts[1])
		);
	}

	private static IList<RecipeEntry> ParseInputs(string s)
	{
		string[] parts = s.Split(',', StringSplitOptions.TrimEntries);
		return parts.Select(ParseIngredient).ToList();
	}

	private static RecipeEntry ParseOutput(string s)
	{
		return ParseIngredient(s);
	}

	private static RecipeEntry ParseIngredient(string s)
	{
		string[] parts = s.Split(' ', StringSplitOptions.TrimEntries);
		if (parts.Length != 2)
		{
			throw new FormatException();
		}
		return new RecipeEntry
		{
			Chemical = new Chemical(parts[1]),
			Quantity = int.Parse(parts[0])
		};
	}

	public override string ToString()
	{
		string inputsStr = string.Join(", ", _inputs.Select(r => $"{r.Quantity} {r.Chemical}"));
		string outputStr = $"{_output.Quantity} {_output.Chemical}";
		return $"{inputsStr} => {outputStr}";
	}
}
