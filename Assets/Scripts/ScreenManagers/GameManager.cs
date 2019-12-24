using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace ScreenManagers
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private string databasePath;
        [SerializeField] private RecipesListScreen recipesListScreen;
        [SerializeField] private StorageScreen storageScreen;
        [SerializeField] private OrdersListScreen ordersListScreen;
        
        private Dictionary<ScreenManagerType, ScreenManager> screenManagers;

        public Database Database { get; private set; }
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Database = LoadDatabase(databasePath) ?? new Database();
            
            Instance = this;
            recipesListScreen.StorageButtonClicked += SetStorageScreenActive;
            recipesListScreen.OrdersButtonClicked += SetOrdersListScreenActive;
            storageScreen.BackButtonClicked += SetRecipesListScreenActive;
            ordersListScreen.BackButtonClicked += SetRecipesListScreenActive;
            screenManagers = new Dictionary<ScreenManagerType, ScreenManager>
            {
                {ScreenManagerType.RecipesListScreen, recipesListScreen},
                {ScreenManagerType.StorageScreen, storageScreen},
                {ScreenManagerType.OrdersListScreen, ordersListScreen},
            };
            SetRecipesListScreenActive();
        }

        private void OnDestroy()
        {
            if (Database != null) SaveDatabase(databasePath, Database);
        }

        private static Database LoadDatabase(string path)
        {
            if (!Directory.Exists(path)) return null;
            var filePath = Path.Combine(path, "database.json");
            if (!File.Exists(filePath)) return null;
            var databaseContent = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(databaseContent)) return null;
            return JsonConvert.DeserializeObject<Database>(databaseContent);
        }

        private static void SaveDatabase(string path, Database database)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Debug.LogError("Database path is invalid");
                return;
            }
            var databaseJson = JsonConvert.SerializeObject(database, Formatting.Indented);
            File.WriteAllText(Path.Combine(path, "database.json"), databaseJson);
        }

        private void SetRecipesListScreenActive()
        {
            screenManagers.ToList()
                .ForEach(s =>
                {
                    if (s.Key == ScreenManagerType.RecipesListScreen) s.Value.Show();
                    else s.Value.Hide();
                });
        }
        
        private void SetOrdersListScreenActive()
        {
            screenManagers.ToList()
                .ForEach(s =>
                {
                    if (s.Key == ScreenManagerType.OrdersListScreen) s.Value.Show();
                    else s.Value.Hide();
                });
        }
    
        private void SetStorageScreenActive()
        {
            screenManagers.ToList()
                .ForEach(s =>
                {
                    if (s.Key == ScreenManagerType.StorageScreen) s.Value.Show();
                    else s.Value.Hide();
                });
        }
    }

    public enum ScreenManagerType
    {
        RecipesListScreen,
        StorageScreen,
        OrdersListScreen
    }
}