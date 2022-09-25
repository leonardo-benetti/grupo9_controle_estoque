using System;
using System.Collections.Generic;
using System.Linq;
using grupo9_controle_estoque.Model;

namespace grupo9_controle_estoque.Controller;
internal class ProductCrontroller
{
    private readonly ApplicationDbContext context;
    public ProductCrontroller(ApplicationDbContext context)
    {
        this.context = context;
    }
    public List<Product> GetProducts()
    {
        return this.context.Products.ToList();
    }
}