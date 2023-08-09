using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class CategoriesProduct
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? ProductId { get; set; }
}
