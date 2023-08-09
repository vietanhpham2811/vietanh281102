using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class TagsProduct
{
    public int Id { get; set; }

    public int? TagId { get; set; }

    public int? ProductId { get; set; }
}
