using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class OrdersDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }
}
