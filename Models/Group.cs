using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QlBanOpDaDienThoai.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Arrange { get; set; }
    }
}
