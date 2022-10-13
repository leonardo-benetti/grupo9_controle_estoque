using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace grupo9_controle_estoque.Model;

[Table("Notification")]
public class Notification
{
    [Key]
    [Column(Order = 1)]
    public int? UserID { get; set; } = null;

    [Key]
    [Column(Order = 2)]
    public string ProductID { get; set; } = string.Empty;

    [Column(TypeName = "INT")]
    public int? MinQuantity { get; set; } = null;

}

