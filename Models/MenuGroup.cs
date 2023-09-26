using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QlBanOpDaDienThoai.Models
{
    [Table("MenusGroups")]
    public class MenuGroup
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int GroupId { get; set; }
    }
}
