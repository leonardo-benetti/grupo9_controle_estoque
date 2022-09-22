using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grupo9_controle_estoque.Model
{
    [Table("Products")]
    internal class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GUID { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Description { get; set; }

        [Column(TypeName = "FLOAT")]
        public float Price { get; set; }

        [Column(TypeName = "INT")]
        public int Quantity { get; set; }

        [Column(TypeName = "VARCHAR(150)")]
        public string Category { get; set; }

    }
}
