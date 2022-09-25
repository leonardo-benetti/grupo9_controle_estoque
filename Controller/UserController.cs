using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grupo9_controle_estoque.Model;

namespace grupo9_controle_estoque.Controller;
internal class UserController
{
    private readonly ApplicationDbContext context;
    public UserController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public List<User> GetUsers()
    {
        return this.context.Users.ToList();
    }

}