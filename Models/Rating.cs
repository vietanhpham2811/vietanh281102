using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? Star { get; set; }
}
