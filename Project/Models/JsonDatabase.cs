using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ListItem
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class JsonDatabase
    {
        private readonly string _filePath;
        private List<ListItem> _items;

        public JsonDatabase(string filePath = "lists.json")
        {
            _filePath = filePath;
            _items = new List<ListItem>();
            LoadData();
        }

        private void LoadData()
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
                    // Если файл содержит некорректный JSON, создаем новый пустой список
                    _items = new List<ListItem>();
                    // Перезаписываем файл с корректным JSON
                    SaveData().Wait();
                }
            }
            else
            {
                _items = new List<ListItem>();
                // Создаем файл с пустым массивом
                SaveData().Wait();
            }
        }

        private async Task SaveData()
        {
            string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<ListItem> AddItem(string title, string description)
        {
            // Генерируем новый id как двузначное число
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
                Description = description
            };

            _items.Add(item);
            await SaveData();
            return item;
        }

        public List<ListItem> GetAllItems()
        {
            return _items;
        }

        public async Task<bool> DeleteItem(string id)
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

        public async Task<bool> UpdateItem(string id, string title, string description)
        {
            var item = _items.Find(x => x.Id == id);
            if (item != null)
            {
                item.Title = title;
                item.Description = description;
                await SaveData();
                return true;
            }
            return false;
        }
    }
} 