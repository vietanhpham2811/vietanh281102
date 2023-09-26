using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlBanOpDaDienThoai.Models
{
    public partial class Function
    {
        public int Id { get; set; } 
        public string NamePermission { get; set; }    
        public string IdPermission { get; set; }
        //public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    }
}
