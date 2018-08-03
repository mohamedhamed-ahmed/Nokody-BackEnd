using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nokody.API.Domain.Model.Models
{
  [Table("Transaction")]
  public partial class Transaction
  {
    public int Id { get; set; }

    public int? FromAccountId { get; set; }

    public int? ToAccountId { get; set; }

    public float? Amount { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsSuccessful { get; set; }

    public virtual Account FromAccount { get; set; }
    public virtual Account ToAccount { get; set; }

  }
}
