using System;
using System.Collections.Generic;
using System.Text;

namespace Nokody.API.Domain.Model
{
  public class TransactionSummary
  {
    public string FromAccount { get; set; }
    public string ToAccount { get; set; }
    public float Amount { get; set; }
    public int PinCode { get; set; }
  }
}
