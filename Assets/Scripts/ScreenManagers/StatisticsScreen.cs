using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace ScreenManagers
{
    public class StatisticsScreen : ScreenManager
    {
        public event Action BackButtonClicked;
        
        [SerializeField] private Button backButton;
        [SerializeField] private ComponentView componentViewPrefab;
        [SerializeField] private Transform componentParent;
        
        private List<RecipeComponent> ingredients = new List<RecipeComponent>();
        
        private ComponentView target;
        private List<ComponentView> componentViews = new List<ComponentView>();
        
        public override void Show()
        {
            gameObject.SetActive(true);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(OnBackButtonClicked);

            ingredients = GameManager.Instance.Database.Orders.SelectMany(o =>
                    o.dishes.Select(GameManager.Instance.Database.GetRecipe).SelectMany(r => r.recipeComponents))
                .GroupBy(c => c.ingredientId).Select(g => new RecipeComponent
                    {ingredientId = g.Key, weight = g.Sum(c => c.weight)}).ToList();
            componentViews.ForEach(c => Destroy(c.gameObject));
            ingredients.ForEach(AddView);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        private void AddView(RecipeComponent recipeComponent)
        {
            var view = Instantiate(componentViewPrefab, componentParent);
            view.Show(recipeComponent);
            componentViews.Add(view);
        }

        private void OnBackButtonClicked()
        {
            Hide();
            BackButtonClicked?.Invoke();
        }
    }
}