using System;
using System.Globalization;
using Models;
using ScreenManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class ComponentView : MonoBehaviour
	{
		public event Action<RecipeComponent, ComponentView> RemoveButtonClicked;

		[SerializeField] private Button removeButton;
		[SerializeField] private TMP_Text ingredientNameText;
		[SerializeField] private TMP_Text weightText;


		private RecipeComponent target;

		public void Show(RecipeComponent newTarget)
		{
			gameObject.SetActive(true);
			target = newTarget;

			removeButton.onClick.RemoveAllListeners();
			removeButton.onClick.AddListener(OnRemoveButtonClicked);

			var ingredient = target.Ingredient;
			ingredientNameText.text = ingredient?.ingredientName ?? "--";
			weightText.text = ingredient != null ? string.Format(CultureInfo.GetCultureInfo("uk-UA"), "{0:F3} кг", target.weight) : "--";
		}

		private void OnRemoveButtonClicked() => RemoveButtonClicked?.Invoke(target, this);

		public void Hide() => gameObject.SetActive(false);
	}
}