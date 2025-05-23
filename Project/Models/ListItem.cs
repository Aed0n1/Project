using System.Collections.Generic;

namespace Project.Models
{
    public class ListItem
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ListProduct> Products { get; set; } = new List<ListProduct>();
    }
} 