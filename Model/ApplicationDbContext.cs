using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace grupo9_controle_estoque.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        char sep = Path.DirectorySeparatorChar;
        CreatePath($"PersistentData{sep}DB");
        CreatePath($"PersistentData{sep}ProfilePictures");
        Database.EnsureCreated();
    }

    public static void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Product> Products { get; set; }


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
        new Product
        {
            GUID = Guid.NewGuid().ToString(),
            Name = "Livro",
            Description = "O retorno do rei",
            Price = 50,
            Quantity = 5,
            Category = "Fantasia"
        }); ;

        base.OnModelCreating(modelBuilder);
    }
}
