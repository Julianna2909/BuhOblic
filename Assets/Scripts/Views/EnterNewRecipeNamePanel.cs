using System;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public sealed class EnterNewRecipeNamePanel : MonoBehaviour
	{
		public event Action<Recipe> RecipeCreated;

		[SerializeField] private TMP_InputField newRecipeName;
		[SerializeField] private Button createButton;
		[SerializeField] private Button cancelButton;

		private int newId;

		private void Awake()
		{
			createButton.onClick.AddListener(OnCreateButtonClicked);
			cancelButton.onClick.AddListener(OnCancelButtonClicked);
		}

		private void OnCancelButtonClicked()
		{
			RecipeCreated = null;
			Hide();
		}

		private void OnCreateButtonClicked()
		{
			if (string.IsNullOrWhiteSpace(newRecipeName.text)) return;
			var recipe = new Recipe {id = newId, recipeName = newRecipeName.text};
			RecipeCreated?.Invoke(recipe);
			Hide();
		}

		public void Show(int id)
		{
			newId = id;
			newRecipeName.text = "";
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}