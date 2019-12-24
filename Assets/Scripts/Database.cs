using System;
using System.Collections.Generic;
using System.Linq;
using Models;

[Serializable]
public class Database
{
	public List<Recipe> Recipes { get; private set; }
	public List<Ingredient> Ingredients { get; private set; }
	public List<Order> Orders { get; private set; }

	public Database()
	{
		Recipes = new List<Recipe>();
		Ingredients = new List<Ingredient>();
		Orders = new List<Order>();
	}

	public Ingredient GetIngredient(int id) => Ingredients.FirstOrDefault(i => i.id == id);
	public Recipe GetRecipe(int id) => Recipes.FirstOrDefault(r => r.id == id);
}