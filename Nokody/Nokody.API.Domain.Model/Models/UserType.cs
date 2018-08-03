using System.ComponentModel.DataAnnotations.Schema;

namespace Nokody.API.Domain.Model.Models
{
  [Table("UserType")]
  public partial class UserType
  {

    public int Id { get; set; }

    // [StringLength(50)]
    public string Name { get; set; }

    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //public virtual ICollection<User> Users { get; set; }
  }
}
