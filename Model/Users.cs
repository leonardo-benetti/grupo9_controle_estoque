using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grupo9_controle_estoque.Model
{
    [Table("Users")]
    internal class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Pwd_salt { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Pwd_hash { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Profile_pic { get; set; }

    }
}
