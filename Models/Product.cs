﻿using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string? Photo { get; set; }

    public int? Hot { get; set; }

    public double? Price { get; set; }

    public double? Discount { get; set; }
}
