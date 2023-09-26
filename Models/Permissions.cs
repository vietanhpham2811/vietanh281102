using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace QlBanOpDaDienThoai.Models
{
    [Table("Permissions")]
    public partial class Permissions
    {
        [Key]
        public int Id { get; set; }
      
        public int? IdUser { get; set; } 
        public int? IdFunction { get; set; }
        public string? Note { get; set; }

        //public virtual Function Id { get; set; } = null!;

        //public virtual User User { get; set; } = null!;
    }
}
