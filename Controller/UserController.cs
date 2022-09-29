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

    public bool CreateUser(string user_name, string password)
    {
        try
        {
            User newUser = new User(user_name, password);
            this.Users.Add(newUser);
            this.context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public void DeleteUser(User userToDelete)
    {
        this.Users.Remove(userToDelete);
        context.SaveChanges();
    }
    public bool Logon(string username, string password)
    {
        try
        {
            User? db_user = this.Users.Where(user => user.Name == username).FirstOrDefault();
            if (db_user == null)
                return false;
            string attempt_password_hash = User.hashPassword(db_user.Pwd_salt, password);
            if (attempt_password_hash == db_user.Pwd_hash)
                return true;
            return false;
        }
        catch
        {
            return false;
        }
    }
}