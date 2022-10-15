using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace grupo9_controle_estoque.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        CreatePath(Path.Combine(Path.GetFullPath(@"..\..\..\"), "PersistentData", "DB"));
        CreatePath(Path.Combine(Path.GetFullPath(@"..\..\..\"), "PersistentData", "ProfilePictures"));
        Database.EnsureCreated();
    }

    public static void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Notification> Notification { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        string adm_salt = User.generateSalt();
        string adm_hashed_pwd = User.hashPassword(adm_salt, "admin");

        modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            Name = "Admin",
            Pwd_salt = adm_salt,
            Pwd_hash = adm_hashed_pwd,
            Profile_pic = ""
        });

        modelBuilder.Entity<Product>().HasData(
            new Product[] {
                new Product
                {
                    Id = 1,
                    Name = "O retorno do rei",
                    Description = "Livro, Tolkien",
                    Price = 50,
                    Quantity = 5,
                    Category = "Livro"
                },
                new Product
                {
                    Id = 2,
                    Name = "A riqueza das nações",
                    Description = "Livro, Adam Smith",
                    Price = 35,
                    Quantity = 8,
                    Category = "Livro"
                },
                new Product
                {
                    Id = 3,
                    Name = "Smart TV Samsung",
                    Description = "58 Polegadas",
                    Price = 2200,
                    Quantity = 3,
                    Category = "TV"
                }
            });

        modelBuilder.Entity<Notification>().HasData(
        new Notification
        {
            UserID = 1,
            ProductID = 1,
            MinQuantity = 5,
        });

        modelBuilder.Entity<Notification>().HasKey(notification => new { notification.UserID, notification.ProductID });

        base.OnModelCreating(modelBuilder);
    }
}
