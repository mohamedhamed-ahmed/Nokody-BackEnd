using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nokody.API.Domain.Model.Models
{
  [Table("Account")]
  public partial class Account
  {
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PinCode { get; set; }

    public float? Balance { get; set; }

    public bool? IsActive { get; set; }

    public string PassportNumber { get; set; }

    public string BraceletNumber { get; set; }

    public DateTime? OpenedDate { get; set; }

    public virtual User User { get; set; }
  }
}
