using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;


namespace ScreenManagers
{
	public sealed class StorageScreen : ScreenManager
	{
		public event Action BackButtonClicked;
		
		[SerializeField] private Button backButton;
		[SerializeField] private Button addButton;
		[SerializeField] private EnterNewIngredientPanel newIngredientPanel;
		[SerializeField] private IngredientView ingredientViewPrefab;
		[SerializeField] private Transform ingTable;

		private List<IngredientView> ingredientsViews;

		public override void Show()
		{
			gameObject.SetActive(true);
			newIngredientPanel.Hide();
			
			backButton.onClick.RemoveAllListeners();
			backButton.onClick.AddListener(OnBackButtonClicked);
			addButton.onClick.RemoveAllListeners();
			addButton.onClick.AddListener(OnAddButtonClicked);

			ingredientsViews?.ForEach(v => Destroy(v.gameObject));
			ingredientsViews = new List<IngredientView>();
			GameManager.Instance.Database.Ingredients.ForEach(AddView);
		}

		public override void Hide() => gameObject.SetActive(false);

		private void AddView(Ingredient ingredient)
		{
			var view = Instantiate(ingredientViewPrefab, ingTable);
			view.RemoveButtonClicked += OnRemoveButtonClicked;
			view.Show(ingredient);
			ingredientsViews.Add(view);
		}

		private void AddIngredient(Ingredient ingredient)
		{
			newIngredientPanel.IngredientCreated -= AddIngredient;
			GameManager.Instance.Database.Ingredients.Add(ingredient);
			AddView(ingredient);
		}

		private void OnRemoveButtonClicked(Ingredient target, IngredientView view)
		{
			GameManager.Instance.Database.Ingredients.Remove(target);
			ingredientsViews.Remove(view);
			view.RemoveButtonClicked -= OnRemoveButtonClicked;
			Destroy(view.gameObject);
		}

		private void OnAddButtonClicked()
		{
			newIngredientPanel.Show(GameManager.Instance.Database.Ingredients.LastOrDefault()?.id + 1 ?? 0);
			newIngredientPanel.IngredientCreated += AddIngredient;
		}

		private void OnBackButtonClicked()
		{
			Hide();
			BackButtonClicked?.Invoke();
		}
	}
}