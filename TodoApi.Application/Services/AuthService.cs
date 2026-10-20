using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Data;
using TodoApi.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;

namespace TodoApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public AuthService(AppDbContext dbContext, IConfiguration  configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already exists");
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var jwtkey = _configuration["jwt:key"]
            ?? throw new InvalidOperationException("jwt:Key not found in configuration");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issuer"],
            audience: _configuration["jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["jwt:ExpireMinutes"])),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    
    public async Task<string> LoginAsync(LoginDto dto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.Password))
        {
            throw new Exception("Wrong password");
            
        }
        return GenerateToken(existingUser);
    }
}