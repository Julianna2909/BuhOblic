using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace ScreenManagers
{
	public sealed class RecipesListScreen : ScreenManager
	{
		public event Action StorageButtonClicked;
		public event Action OrdersButtonClicked;

		[SerializeField] private RecipeScreen recipeScreen;
		[SerializeField] private EnterNewRecipeNamePanel newRecipeNamePanel;
		[SerializeField] private Button addRecipeButton;
		[SerializeField] private Button storageButton;
		[SerializeField] private Button ordersButton;
		[SerializeField] private RecipeView recipeViewPrefab;
		[SerializeField] private Transform recipeViewTable;

		private List<RecipeView> recipeViews;

		public override void Show()
		{
			gameObject.SetActive(true);
			newRecipeNamePanel.Hide();

			addRecipeButton.onClick.RemoveAllListeners();
			addRecipeButton.onClick.AddListener(OnAddButtonClicked);
			storageButton.onClick.RemoveAllListeners();
			storageButton.onClick.AddListener(OnStorageButtonClicked);
			ordersButton.onClick.RemoveAllListeners();
			ordersButton.onClick.AddListener(OnOrdersButtonClicked);

			recipeViews?.ForEach(v => Destroy(v.gameObject));
			recipeViews = new List<RecipeView>();
			GameManager.Instance.Database.Recipes.ForEach(AddView);
		}

		private void OnOrdersButtonClicked() => OrdersButtonClicked?.Invoke();

		public override void Hide() => gameObject.SetActive(false);

		private void AddView(Recipe recipe)
		{
			var view = Instantiate(recipeViewPrefab, recipeViewTable);
			view.RemoveButtonClicked += OnRemoveButtonClicked;
			view.RecipeClicked += ShowRecipeScreen;
			view.Show(recipe);
			recipeViews.Add(view);
		}

		private void ShowRecipeScreen(Recipe recipe) => recipeScreen.Show(recipe);

		private void AddRecipe(Recipe recipe)
		{
			newRecipeNamePanel.RecipeCreated -= AddRecipe;
			GameManager.Instance.Database.Recipes.Add(recipe);
			AddView(recipe);
		}

		private void OnRemoveButtonClicked(Recipe target, RecipeView view)
		{
			GameManager.Instance.Database.Recipes.Remove(target);
			recipeViews.Remove(view);
			view.RemoveButtonClicked -= OnRemoveButtonClicked;
			Destroy(view.gameObject);
		}

		private void OnAddButtonClicked()
		{
			newRecipeNamePanel.Show(GameManager.Instance.Database.Recipes.LastOrDefault()?.id + 1 ?? 0);
			newRecipeNamePanel.RecipeCreated += AddRecipe;
		}

		private void OnStorageButtonClicked() => StorageButtonClicked?.Invoke();
	}
}