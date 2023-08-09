using System;
using System.Collections.Generic;

namespace QlBanOpDaDienThoai.Models;

public partial class Slide
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public string? Title { get; set; }

    public string? SubTitle { get; set; }

    public string? Info { get; set; }

    public string? Link { get; set; }
}
