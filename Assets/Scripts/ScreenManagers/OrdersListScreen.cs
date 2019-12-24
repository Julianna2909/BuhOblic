using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace ScreenManagers
{
    public sealed class OrdersListScreen : ScreenManager
    {
        public event Action BackButtonClicked;

        [SerializeField] private OrderScreen orderScreen;
        [SerializeField] private StatisticsScreen statisticsScreen;
        [SerializeField] private Button addOrderButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button deleteAllButton;
        [SerializeField] private Button reportButton;
        [SerializeField] private OrderView orderViewPrefab;
        [SerializeField] private Transform ordersViewTable;
        
        private List<OrderView> orderViews;
        
        public override void Show()
        {
            gameObject.SetActive(true);

            addOrderButton.onClick.RemoveAllListeners();
            addOrderButton.onClick.AddListener(OnAddButtonClicked);
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(OnBackButtonClicked);
            deleteAllButton.onClick.AddListener(OnDeleteAllButtonClicked);
            reportButton.onClick.AddListener(OnReportButtonClicked);

            orderViews?.ForEach(v => Destroy(v.gameObject));
            orderViews = new List<OrderView>();
            GameManager.Instance.Database.Orders.ForEach(AddView);
        }

        private void OnReportButtonClicked()
        {
            statisticsScreen.Show();
        }

        private void OnDeleteAllButtonClicked()
        {
            for (int i = 0; i < GameManager.Instance.Database.Orders.Count; i++)
            {
                GameManager.Instance.Database.Orders.Remove(GameManager.Instance.Database.Orders[i]);
                var view = orderViews[i];
                orderViews.Remove(view);
                view.RemoveButtonClicked -= OnRemoveButtonClicked;
                Destroy(view.gameObject);
            }
        }

        public override void Hide() => gameObject.SetActive(false);

        private void AddView(Order order)
        {
            var view = Instantiate(orderViewPrefab, ordersViewTable);
            view.RemoveButtonClicked += OnRemoveButtonClicked;
            view.RecipeClicked += ShowRecipeScreen;
            view.Show(order);
            orderViews.Add(view);
        }

        private void ShowRecipeScreen(Order order) => orderScreen.Show(order);
        
        private void OnRemoveButtonClicked(Order target, OrderView view)
        {
            GameManager.Instance.Database.Orders.Remove(target);
            orderViews.Remove(view);
            view.RemoveButtonClicked -= OnRemoveButtonClicked;
            Destroy(view.gameObject);
        }

        private void OnAddButtonClicked()
        {
            var order = new Order {id = GameManager.Instance.Database.Orders.LastOrDefault()?.id + 1 ?? 0};
            GameManager.Instance.Database.Orders.Add(order);
            AddView(order);
        }

        private void OnBackButtonClicked() => BackButtonClicked?.Invoke();
    }
}