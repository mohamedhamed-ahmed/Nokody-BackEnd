using System.ComponentModel.DataAnnotations.Schema;

namespace Nokody.API.Domain.Model.Models
{
  [Table("UserIdentity")]
  public partial class UserIdentity
  {
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

  }
}
