using System;
using System.Globalization;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public sealed class IngredientView : MonoBehaviour
	{
		public event Action<Ingredient, IngredientView> RemoveButtonClicked;
		
		[SerializeField] private TMP_Text idText;
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private TMP_Text priceText;
		[SerializeField] private Button removeButton;

		private Ingredient target;

		public void Show(Ingredient newTarget)
		{
			gameObject.SetActive(true);

			target = newTarget;
			idText.text = target.id.ToString();
			nameText.text = target.ingredientName;
			priceText.text = string.Format(CultureInfo.GetCultureInfo("uk-UA"), "{0:C}", target.price);

			removeButton.onClick.RemoveAllListeners();
			removeButton.onClick.AddListener(() => RemoveButtonClicked?.Invoke(target, this));
		}
		
		public void Hide() => gameObject.SetActive(false);
	}
}