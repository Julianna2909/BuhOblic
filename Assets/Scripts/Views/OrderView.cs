using System;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class OrderView : MonoBehaviour
    {
        public event Action<Order> RecipeClicked;
        public event Action<Order, OrderView> RemoveButtonClicked;
		
        [SerializeField] private TMP_Text idText;
        [SerializeField] private Button showButton;
        [SerializeField] private Button removeButton;
		
        private Order target;

        public void Show(Order newTarget)
        {
            gameObject.SetActive(true);
            target = newTarget;
            idText.text = target.id.ToString();

            showButton.onClick.RemoveAllListeners();
            showButton.onClick.AddListener(OnShowButtonClicked);
            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => RemoveButtonClicked?.Invoke(target, this));
        }
        private void OnShowButtonClicked() => RecipeClicked?.Invoke(target);
    }
}