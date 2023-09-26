using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QlBanOpDaDienThoai.Models
{
    [Table("UsersGroups")]
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
