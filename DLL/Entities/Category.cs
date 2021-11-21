﻿using System;

namespace DLL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Bed { get; set; }

        public decimal Price { get; set; }
    }
}
