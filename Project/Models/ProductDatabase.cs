using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Product
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Barcode { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }
    }

    public class ProductDatabase
    {
        private readonly string _filePath;
        private List<Product> _products;

        public ProductDatabase(string filePath = "products.json")
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(baseDirectory, filePath);
            _products = new List<Product>();
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
                        _products = new List<Product>();
                        return;
                    }

                    try
                    {
                        _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                    }
                    catch (JsonException)
                    {
                        _products = new List<Product>();
                        SaveData().Wait();
                    }
                }
                else
                {
                    _products = new List<Product>();
                    SaveData().Wait();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading products data: {ex.Message}", ex);
            }
        }

        private async Task SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving products data: {ex.Message}", ex);
            }
        }

        public async Task<Product> AddProduct(string name, string type, string barcode, double protein, double fats, double carbohydrates, double calories)
        {
            try
            {
                // Проверяем, существует ли продукт с таким штрих-кодом
                if (_products.Exists(p => p.Barcode == barcode))
                {
                    throw new Exception("Product with this barcode already exists");
                }

                int maxId = 0;
                if (_products.Count > 0)
                {
                    foreach (var existingProduct in _products)
                    {
                        if (int.TryParse(existingProduct.Id, out int idNum))
                        {
                            if (idNum > maxId) maxId = idNum;
                        }
                    }
                }
                string newId = (maxId + 1).ToString("D2");

                var newProduct = new Product
                {
                    Id = newId,
                    Name = name,
                    Type = type,
                    Barcode = barcode,
                    Protein = protein,
                    Fats = fats,
                    Carbohydrates = carbohydrates,
                    Calories = calories
                };

                _products.Add(newProduct);
                await SaveData();
                return newProduct;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding product: {ex.Message}", ex);
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductByBarcode(string barcode)
        {
            return _products.Find(p => p.Barcode == barcode);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                var product = _products.Find(x => x.Id == id);
                if (product != null)
                {
                    _products.Remove(product);
                    await SaveData();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting product: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateProduct(string id, string name, string type, string barcode, double protein, double fats, double carbohydrates, double calories)
        {
            try
            {
                var product = _products.Find(x => x.Id == id);
                if (product != null)
                {
                    // Проверяем, не пытаемся ли мы установить штрих-код, который уже существует у другого продукта
                    if (barcode != product.Barcode && _products.Exists(p => p.Barcode == barcode))
                    {
                        throw new Exception("Product with this barcode already exists");
                    }

                    product.Name = name;
                    product.Type = type;
                    product.Barcode = barcode;
                    product.Protein = protein;
                    product.Fats = fats;
                    product.Carbohydrates = carbohydrates;
                    product.Calories = calories;
                    await SaveData();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product: {ex.Message}", ex);
            }
        }
    }
} 