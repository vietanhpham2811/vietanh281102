﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QlBanOpDaDienThoai.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string Link { get; set; }
        public double Arrange { get; set; }
    }
}
