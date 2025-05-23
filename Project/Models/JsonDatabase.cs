using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Models
{
    public class JsonDatabase
    {
        private readonly string _filePath;
        private List<ListItem> _items;

        public JsonDatabase(string filePath = "lists.json")
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(baseDirectory, filePath);
            _items = new List<ListItem>();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        _items = new List<ListItem>();
                        return;
                    }

                    try
                    {
                        _items = JsonSerializer.Deserialize<List<ListItem>>(json) ?? new List<ListItem>();
                    }
                    catch (JsonException)
                    {
                        _items = new List<ListItem>();
                        SaveData().Wait();
                    }
                }
                else
                {
                    _items = new List<ListItem>();
                    SaveData().Wait();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading data: {ex.Message}", ex);
            }
        }

        private async Task SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving data: {ex.Message}", ex);
            }
        }

        public async Task<ListItem> AddItem(string title, string description)
        {
            try
            {
                int maxId = 0;
                if (_items.Count > 0)
                {
                    foreach (var currentItem in _items)
                    {
                        if (int.TryParse(currentItem.Id, out int idNum))
                        {
                            if (idNum > maxId) maxId = idNum;
                        }
                    }
                }
                string newId = (maxId + 1).ToString("D2");

                var item = new ListItem
                {
                    Id = newId,
                    Title = title,
                    Description = description,
                    Products = new List<ListProduct>()
                };

                _items.Add(item);
                await SaveData();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding element: {ex.Message}", ex);
            }
        }

        public List<ListItem> GetAllItems()
        {
            return _items;
        }

        public async Task<bool> DeleteItem(string id)
        {
            try
            {
                var item = _items.Find(x => x.Id == id);
                if (item != null)
                {
                    _items.Remove(item);
                    await SaveData();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting item: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateItem(string id, string title, List<ListProduct> products)
        {
            try
            {
                var item = _items.Find(x => x.Id == id);
                if (item != null)
                {
                    item.Title = title;
                    item.Products = products;
                    await SaveData();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating item: {ex.Message}", ex);
            }
        }

        public ListItem GetItem(string id)
        {
            try
            {
                return _items.Find(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting item: {ex.Message}", ex);
            }
        }
    }
} 