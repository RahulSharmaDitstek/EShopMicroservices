﻿namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = [];
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}
