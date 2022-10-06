using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace grupo9_controle_estoque.Model;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Column(TypeName = "VARCHAR(150)")]
    public string Name { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "VARCHAR(150)")]
    public string Pwd_salt { get; set; } = string.Empty;

    [Column(TypeName = "VARCHAR(150)")]
    public string Pwd_hash { get; set; } = string.Empty;

    [Column(TypeName = "VARCHAR(150)")]
    public string Profile_pic { get; set; } = string.Empty;

    public User()
    {
        Name = "Usuário não logado";
    }
    public User(string Name, string Password)
    {
        this.Name = Name;
        this.Pwd_salt = generateSalt();
        this.Pwd_hash = hashPassword(this.Pwd_salt, Password);
        this.Profile_pic = string.Empty;
    }
    public static string generateSalt()
    {
        byte[] buffer = RandomNumberGenerator.GetBytes(8);
        return BitConverter.ToString(buffer).Replace("-", "");
    }

    public static string hashPassword(string salt, string password)
    {
        string saltedPassword = password + salt;

        byte[] plaintextBytes = Encoding.UTF8.GetBytes(saltedPassword);
        SHA512 mySHA2_512 = SHA512.Create();
        byte[] output = mySHA2_512.ComputeHash(plaintextBytes);

        return BitConverter.ToString(output).Replace("-","");
    }

}
