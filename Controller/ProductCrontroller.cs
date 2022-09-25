using System;
using System.Collections.Generic;
using System.Linq;
using grupo9_controle_estoque.Model;
using Microsoft.EntityFrameworkCore;
namespace grupo9_controle_estoque.Controller;
internal class ProductCrontroller
{
    private readonly ApplicationDbContext context;
    private DbSet<Product> Products;
    public ProductCrontroller(ApplicationDbContext context)
    {
        this.context = context;
        this.Products = this.context.Products;
    }
    public List<Product> GetProducts()
    {
        return this.context.Products.ToList();
    }
    public void DeleteProduct(Product selectedProduct)
    {
        Products.Remove(selectedProduct);
        context.SaveChanges();
    }
    public void AddItem(Product newProduct)
    {
        newProduct.GUID = Guid.NewGuid().ToString();
        Products.Add(newProduct);
        context.SaveChanges();
    }
    public void EditProduct(string id, Product newProductValues)
    {
        Product selectedProduct = Products.Where(product => product.GUID == id).Single();
        changeProductValues(ref selectedProduct, newProductValues);
        context.SaveChanges();
    }
    private void changeProductValues(ref Product oldProductValues, Product newProductValues)
    {
        oldProductValues.Name = newProductValues.Name;
        oldProductValues.Description = newProductValues.Description;
        oldProductValues.Price = newProductValues.Price;
        oldProductValues.Quantity = newProductValues.Quantity;
        oldProductValues.Category = newProductValues.Category;
    }
}