using System.ComponentModel.DataAnnotations.Schema;

namespace Nokody.API.Domain.Model.Models
{
  [Table("User")]
  public partial class User
  {
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    public int? UserTypeId { get; set; }
    public int? UserIdentityId { get; set; }
    public string DeviceId { get; set; }
    public virtual UserType UserType { get; set; }

    public virtual UserIdentity UserIdentity { get; set; }
  }
}
