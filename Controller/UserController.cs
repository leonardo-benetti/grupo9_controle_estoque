using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grupo9_controle_estoque.Model;
using Microsoft.EntityFrameworkCore;

namespace grupo9_controle_estoque.Controller;
internal class UserController
{
    private readonly ApplicationDbContext context;
    private DbSet<User> Users;
    public UserController(ApplicationDbContext context)
    {
        this.context = context;
        this.Users = this.context.Users;
    }
    public List<User> GetUsers()
    {
        return this.context.Users.ToList();
    }

    public void CreateUser(User newUser)
    {
        this.Users.Add(newUser);
        GetUsers();
    }

}