using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScreenManagers;

namespace Models
{
	public abstract class BaseRecord
	{
		public int id;
	}

	[Serializable]
	public sealed class Ingredient : BaseRecord
	{
		public string ingredientName;
		public float price;
	}

	[Serializable]
	public sealed class Recipe : BaseRecord
	{
		public string recipeName;
		public List<RecipeComponent> recipeComponents = new List<RecipeComponent>();

		[JsonIgnore]
		public float TotalPrice => recipeComponents.Count > 0
			? recipeComponents.Sum(c => GameManager.Instance.Database.GetIngredient(c.ingredientId)?.price ?? 0)
			: 0f;
		[JsonIgnore]
		public float TotalWeight => recipeComponents.Count > 0 ? recipeComponents.Sum(c => c.Ingredient != null ? c.weight : 0) : 0f;
	}
	
	[Serializable]
	public sealed class RecipeComponent
	{
		public int ingredientId;
		public float weight;

		[JsonIgnore]
		public Ingredient Ingredient => GameManager.Instance.Database.GetIngredient(ingredientId);
	}
}