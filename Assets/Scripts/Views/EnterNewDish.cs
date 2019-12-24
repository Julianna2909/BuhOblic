using System;
using System.Linq;
using Models;
using ScreenManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class EnterNewDish : MonoBehaviour
    {
        public event Action<Recipe> DishAdded;

        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private Button createButton;
        [SerializeField] private Button cancelButton;

        private void Awake()
        {
            createButton.onClick.AddListener(OnCreateButtonClicked);
            cancelButton.onClick.AddListener(OnCancelButtonClicked);
        }

        public void Show()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(GameManager.Instance.Database.Recipes.Select(i => i.recipeName).ToList());
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);

        private void OnCreateButtonClicked()
        {
            var recipe = GameManager.Instance.Database.Recipes[dropdown.value];
            DishAdded?.Invoke(recipe);
            Hide();
        }

        private void OnCancelButtonClicked()
        {
            DishAdded = null;
            Hide();
        }
    }
}