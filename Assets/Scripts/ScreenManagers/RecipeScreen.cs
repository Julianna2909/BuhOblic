using System;
using System.Collections.Generic;
using System.Globalization;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace ScreenManagers
{
    public sealed class RecipeScreen : ScreenManager
    {
        private event Action BackButtonClicked;
        
        [SerializeField] private Button backButton;
        [SerializeField] private Button addButton;
        [SerializeField] private TMP_Text totalWeightText;
        [SerializeField] private TMP_Text totalPriceText;
        [SerializeField] private ComponentView componentPrefab;
        [SerializeField] private Transform componentsParent;
        [SerializeField] private EnterNewComponentPanel newComponentPanel;
        
        private Recipe target;
        private List<ComponentView> componentViews;

        public override void Show() => Show(null);

        public void Show(Recipe newTarget)
        {
            gameObject.SetActive(true);
            newComponentPanel.Hide();
            
            addButton.onClick.RemoveAllListeners();
            addButton.onClick.AddListener(OnAddButtonClicked);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(OnBackButtonClicked);

            componentViews?.ForEach(v => Destroy(v.gameObject));
            componentViews = new List<ComponentView>();
            target = newTarget;
            target.recipeComponents.ForEach(AddView);
            UpdateTotals();
        }

        public override void Hide() => gameObject.SetActive(false);

        private void UpdateTotals()
        {
            var cultureInfo = CultureInfo.GetCultureInfo("uk-UA");
            totalPriceText.text = string.Format(cultureInfo, "{0:C}", target.TotalPrice);
            totalWeightText.text = string.Format(cultureInfo, "{0:F3} кг", target.TotalWeight);
        }
        
        private void AddView(RecipeComponent recipeComponent)
        {
            var view = Instantiate(componentPrefab, componentsParent);
            view.RemoveButtonClicked += OnRemoveButtonClicked;
            view.Show(recipeComponent);
            componentViews.Add(view);
        }

        private void AddComponent(RecipeComponent component)
        {
            newComponentPanel.ComponentCreated -= AddComponent;
            target.recipeComponents.Add(component);
            AddView(component);
            UpdateTotals();
        }

        private void OnRemoveButtonClicked(RecipeComponent newTarget, ComponentView view)
        {
            target.recipeComponents.Remove(newTarget);
            componentViews.Remove(view);
            view.RemoveButtonClicked -= OnRemoveButtonClicked;
            Destroy(view.gameObject);
        }

        private void OnAddButtonClicked()
        {
            newComponentPanel.Show();
            newComponentPanel.ComponentCreated += AddComponent;
        }
        private void OnBackButtonClicked()
        {
            Hide();
            BackButtonClicked?.Invoke();
        }
    }
}