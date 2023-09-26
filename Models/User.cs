using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    public string? ChucVu { get;set; }
    public int? IdLoaiNhanVien { get; set; }
    //public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();


}
