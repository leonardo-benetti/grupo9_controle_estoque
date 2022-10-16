using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace grupo9_controle_estoque.Model;

[Table("Notification")]
public class Notification
{
    [Key]
    [Column(Order = 1)]
    public uint? UserID { get; set; } = null;

    [Key]
    [Column(Order = 2)]
    public int ProductID { get; set; } = 0;

    [Column(TypeName = "INT")]
    public int? MinQuantity { get; set; } = null;

}

