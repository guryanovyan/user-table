using System;
using JsonApi.Data;
using JsonApi.Dtos;
using JsonApi.Entities;
using JsonApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JsonApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(AppDbContext dbContext) : ControllerBase
{
  private readonly AppDbContext _dbContext = dbContext;

  [HttpGet]
  public async Task<IActionResult> GetUsers()
  {
    try 
    {
      List<UserDto> users = await _dbContext.Users
        .Select(user => user.ToUserDto())
        .ToListAsync();

      return Ok(new { st = users.Count, seznam = users });

    } 
    catch (Exception e) 
    {
      return StatusCode(500, new { messaage = e });
    }
  }

  [HttpPost]
  public async Task<IActionResult> AddUser([FromBody] CreateUserDto newUser)
  {
    try
    {
      User user = newUser.ToEntity();

      _dbContext.Users.Add(user);
      await _dbContext.SaveChangesAsync();

      List<UserDto> users = await _dbContext.Users
        .Select(user => user.ToUserDto())
        .ToListAsync();

      return Ok(new { st = users.Count, seznam = users });

    } 
    catch (Exception e) 
    {
      return StatusCode(500, new { messaage = e });
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id) {
    try
    {
      var userExists = await _dbContext.Users.AnyAsync(user => user.Id == id);
      if (!userExists) 
      {
        return NotFound(new { message = "User not found" });
      }

      await _dbContext.Users
        .Where(user => user.Id == id)
        .ExecuteDeleteAsync();
      
      return NoContent();
      
    } 
    catch (Exception e) 
    {
      return StatusCode(500, new { messaage = e });
    }
  }
}
