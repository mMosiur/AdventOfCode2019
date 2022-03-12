using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day14;

public class FuelCalculator
{
	public IList<Recipe> Recipes { get; }
	public Chemical FuelChemical { get; }
	public Chemical OreChemical { get; }

	public FuelCalculator(IList<Recipe> recipes, Chemical fuelChemical, Chemical oreChemical)
	{
		Recipes = recipes;
		FuelChemical = fuelChemical;
		OreChemical = oreChemical;
	}

	public FuelCalculator(IList<Recipe> recipes, string fuelChemicalName, string oreChemicalName)
		: this(recipes, new Chemical(fuelChemicalName), new Chemical(oreChemicalName))
	{ }

	public long GetRequiredOreForFuel(int fuelAmount)
	{
		IDictionary<Chemical, long> required = new Dictionary<Chemical, long>
		{
			[FuelChemical] = fuelAmount
		};
		while (required.Where(kp => !kp.Key.Equals(OreChemical)).Any(kp => kp.Value > 0))
		{
			Chemical next = required.Where(kp => !kp.Key.Equals(OreChemical)).First(kp => kp.Value > 0).Key;
			long outputQuantity = Recipes.Single(r => r.OutputChemical.Chemical.Equals(next)).OutputChemical.Quantity;
			long neededQuantity = required[next] / outputQuantity + (required[next] % outputQuantity > 0 ? 1 : 0);
			required[next] -= outputQuantity * neededQuantity;
			foreach (RecipeEntry entry in Recipes.Single(r => r.OutputChemical.Chemical.Equals(next)).InputChemicals)
			{
				if (required.ContainsKey(entry.Chemical))
				{
					required[entry.Chemical] += entry.Quantity * neededQuantity;
				}
				else
				{
					required[entry.Chemical] = entry.Quantity * neededQuantity;
				}
			}
		}
		return required[OreChemical];
	}

	public long GetMaxFuelForGivenOre(long oreAmount)
	{
		int high = 1;
		while (GetRequiredOreForFuel(high) <= oreAmount)
		{
			high *= 2;
		}
		int low = 1;

		while (high - low > 1)
		{
			int next = (low + high) / 2;
			if (GetRequiredOreForFuel(next) < oreAmount)
			{
				low = next;
			}
			else
			{
				high = next;
			}
		}

		return low;
	}
}
