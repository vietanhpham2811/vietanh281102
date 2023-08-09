using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlBanOpDaDienThoai.Models;
[Table("adv")]
public partial class Adv
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public int? Position { get; set; }
}
