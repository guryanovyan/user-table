using System.ComponentModel.DataAnnotations;

namespace JsonApi.Dtos;

public record class CreateUserDto(
  [Required][StringLength(50)] string Ime,
  [Required][Range(1, 120)] int Starost
);