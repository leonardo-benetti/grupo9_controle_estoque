using System;
using System.Collections.Generic;
using System.Linq;
using grupo9_controle_estoque.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace grupo9_controle_estoque.Controller;
public class ProductController
{
    private readonly ApplicationDbContext context;
    private DbSet<Product> Products;
    public ProductController(ApplicationDbContext context)
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
        Products.Add(newProduct);
        context.SaveChanges();
    }
    public void EditProduct(int id, Product newProductValues)
    {   
        Product selectedProduct = Products.Where(product => product.Id == id).Single();
        ChangeProductValues(ref selectedProduct, newProductValues);
        context.SaveChanges();
    }
    private void ChangeProductValues(ref Product oldProductValues, Product newProductValues)
    {
        oldProductValues.Name = newProductValues.Name;
        oldProductValues.Description = newProductValues.Description;
        oldProductValues.Price = newProductValues.Price;
        oldProductValues.Quantity = newProductValues.Quantity;
        oldProductValues.Category = newProductValues.Category;
    }
    public Product? SellProduct(int id, int sell_quantity)
    {
        Product? selectedProduct = Products.Where(product => product.Id == id).FirstOrDefault();

        if (selectedProduct != null && sell_quantity > 0 && sell_quantity <= selectedProduct.Quantity)
        {
            selectedProduct.Quantity -= sell_quantity;
            Products.Update(selectedProduct);
            context.SaveChanges();
            return selectedProduct;
        }

        return null;
    }
}