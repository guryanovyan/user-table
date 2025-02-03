using System;
using JsonApi.Dtos;
using JsonApi.Entities;

namespace JsonApi.Mapping;

public static class UserMapping
{
  public static User ToEntity(this CreateUserDto userDto) 
  {
    return new User() 
    {
      Ime = userDto.Ime,
      Starost = userDto.Starost,
    };
  }

  public static UserDto ToUserDto(this User user)
  {
    return new UserDto(
      user.Id,
      user.Ime,
      user.Starost
    );
  }
}
