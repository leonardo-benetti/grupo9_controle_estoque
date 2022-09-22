using System.IO;
using System;
using Microsoft.EntityFrameworkCore;

namespace grupo9_controle_estoque.Model
{
    internal class ApplicationDbContext:DbContext
    {
        public static void CreateFolderAndFile(string path)
        {
            string root_path = path.Split("/")[0];

            if (!Directory.Exists(root_path))
            {
                Directory.CreateDirectory(root_path);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            CreateFolderAndFile("PersistentData/DB");
            CreateFolderAndFile("PersistentData/ProfilePictures");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string pathDB = System.IO.Path.Combine(currentDirectory, "PersistentData/DB/Data");
            Console.WriteLine(pathDB);
            optionsBuilder.UseSqlite($"Data Source={pathDB}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(
            new Users
            {
                Name = "Administrador",
                Pwd_salt = "1231",
                Pwd_hash = "123312",
                Profile_pic = ""
            });

            modelBuilder.Entity<Products>().HasData(
            new Products
            {
                Name = "Livro",
                Description = "O poder do foda-se",
                Price = 50,
                Quantity = 5,
                Category = "Auto Ajuda"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
