using System;
using System.Collections.Generic;
using System.Text;

namespace Nokody.API.Domain.Model
{
  public class Result
  {
    public ResultType ResultType { get; set; }
    public object Object { get; set; }
  }
  public enum ResultType
  {
    Success = 200,
    BadRequest = 400,
    NotFound = 404,
    Duplicated = 409,
    PrerequisitesNeeded = 412,
    InternalServerError = 500
  }
}
