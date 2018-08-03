using System;
using System.Collections.Generic;
using System.Text;

namespace Nokody.API.Domain.Model
{
  public class UserDetails
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? UserTypeId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int? PinCode { get; set; }
    public float? Balance { get; set; }
    public string PassportNumber { get; set; }
    public string BraceletNumber { get; set; }

  }
}
