using System;
using System.Globalization;
using System.Linq;
using Models;
using ScreenManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class EnterNewComponentPanel : MonoBehaviour
	{
		public event Action<RecipeComponent> ComponentCreated;

		[SerializeField] private TMP_Dropdown dropdown;
		[SerializeField] private TMP_InputField weightText;
		[SerializeField] private Button createButton;
		[SerializeField] private Button cancelButton;

		private void Awake()
		{
			createButton.onClick.AddListener(OnCreateButtonClicked);
			cancelButton.onClick.AddListener(OnCancelButtonClicked);
		}

		public void Show()
		{
			weightText.text = "";
			dropdown.ClearOptions();
			dropdown.AddOptions(GameManager.Instance.Database.Ingredients.Select(i => i.ingredientName).ToList());
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);

		private void OnCreateButtonClicked()
		{
			if (string.IsNullOrWhiteSpace(weightText.text)) return;
			var ingredient = GameManager.Instance.Database.Ingredients[dropdown.value];
			var component = new RecipeComponent {ingredientId = ingredient.id};
			float.TryParse(weightText.text, out component.weight);
			float.TryParse(weightText.text, NumberStyles.Float, CultureInfo.InvariantCulture, out component.weight);
			ComponentCreated?.Invoke(component);
			Hide();
		}

		private void OnCancelButtonClicked()
		{
			ComponentCreated = null;
			Hide();
		}
	}
}