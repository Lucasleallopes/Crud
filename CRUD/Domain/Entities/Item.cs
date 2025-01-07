﻿namespace CRUD.Domain.Entities {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}