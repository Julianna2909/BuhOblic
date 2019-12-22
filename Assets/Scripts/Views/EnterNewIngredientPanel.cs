using System;
using System.Globalization;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public sealed class EnterNewIngredientPanel : MonoBehaviour
	{
		public event Action<Ingredient> IngredientCreated;

		[SerializeField] private TMP_InputField newIngredientName;
		[SerializeField] private TMP_InputField newIngredientPrice;
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
			IngredientCreated = null;
			Hide();
		}

		private void OnCreateButtonClicked()
		{
			if (string.IsNullOrWhiteSpace(newIngredientName.text) &&
				string.IsNullOrWhiteSpace(newIngredientPrice.text)) return;
			var ing = new Ingredient {id = newId, ingredientName = newIngredientName.text};
			float.TryParse(newIngredientPrice.text, NumberStyles.Currency, CultureInfo.InvariantCulture, out ing.price);
			IngredientCreated?.Invoke(ing);
			Hide();
		}

		public void Show(int id)
		{
			newId = id;
			newIngredientName.text = "";
			newIngredientPrice.text = "";
			
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}