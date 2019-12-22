using System;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class RecipeView : MonoBehaviour
	{
		public event Action<Recipe> RecipeClicked;
		public event Action<Recipe, RecipeView> RemoveButtonClicked;
		
		[SerializeField] private TMP_Text idText;
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private Button showButton;
		[SerializeField] private Button removeButton;
		
		private Recipe target;

		public void Show(Recipe newTarget)
		{
			gameObject.SetActive(true);
			target = newTarget;
			idText.text = target.id.ToString();
			nameText.text = target.recipeName;
			
			showButton.onClick.RemoveAllListeners();
			showButton.onClick.AddListener(OnShowButtonClicked);
			removeButton.onClick.RemoveAllListeners();
			removeButton.onClick.AddListener(() => RemoveButtonClicked?.Invoke(target, this));
		}
		private void OnShowButtonClicked() => RecipeClicked?.Invoke(target);
	}
}