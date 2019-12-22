using System;
using System.Collections.Generic;
using System.Linq;
using Models;

[Serializable]
public class Database
{
	public List<Recipe> Recipes { get; private set; }
	public List<Ingredient> Ingredients { get; private set; }

	public Database()
	{
		Recipes = new List<Recipe>();
		Ingredients = new List<Ingredient>();
	}

	public Ingredient GetIngredient(int id) => Ingredients.FirstOrDefault(i => i.id == id);
}