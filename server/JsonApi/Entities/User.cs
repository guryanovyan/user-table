using System;

namespace JsonApi.Entities;

public class User
{
  public int Id { get; set; }
  public required string Ime { get; set; }
  public int Starost { get; set; }
}
