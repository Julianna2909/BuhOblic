using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace ScreenManagers
{
    public class OrderScreen : ScreenManager
    {
       public event Action BackButtonClicked;
        
        [SerializeField] private Button backButton;
        [SerializeField] private Button addButton;
        [SerializeField] private EnterNewDish newDishPanel;
        [SerializeField] private RecipeView dishPrefab;
        [SerializeField] private Transform dishParent;

        private Order target;
        private List<RecipeView> dishViews;

        public override void Show() => Show(null);

        public void Show(Order newTarget)
        {
            gameObject.SetActive(true);

            addButton.onClick.RemoveAllListeners(); 
            addButton.onClick.AddListener(OnAddButtonClicked);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(OnBackButtonClicked);

            dishViews?.ForEach(v => Destroy(v.gameObject));
            dishViews = new List<RecipeView>();
            target = newTarget;
            target.dishes.Select(GameManager.Instance.Database.GetRecipe).ToList().ForEach(AddView);
        }

        public override void Hide() => gameObject.SetActive(false);

        private void AddView(Recipe recipe)
        {
            var view = Instantiate(dishPrefab, dishParent);
            view.RemoveButtonClicked += OnRemoveButtonClicked;
            view.Show(recipe);
            dishViews.Add(view);
        }

        private void AddComponent(Recipe dish)
        {
            newDishPanel.DishAdded -= AddComponent;
            target.dishes.Add(dish.id);
            AddView(dish);
        }

        private void OnRemoveButtonClicked(Recipe newTarget, RecipeView view)
        {
            target.dishes.Remove(newTarget.id);
            dishViews.Remove(view);
            view.RemoveButtonClicked -= OnRemoveButtonClicked;
            Destroy(view.gameObject);
        }

        private void OnAddButtonClicked()
        {
            newDishPanel.Show();
            newDishPanel.DishAdded += AddComponent;
        }
        private void OnBackButtonClicked()
        {
            Hide();
            BackButtonClicked?.Invoke();
        }
    }
}