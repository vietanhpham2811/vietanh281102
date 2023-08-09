using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class Category
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? Name { get; set; }

    public int? DisplayHomePage { get; set; }
}
