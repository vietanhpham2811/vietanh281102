using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? Create { get; set; }

    public double? Price { get; set; }

    public int? Status { get; set; }
}
