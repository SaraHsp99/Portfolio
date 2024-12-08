using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Provider.SecurityProvider;
public class JwtService
{
	private readonly string _key;
	private readonly string _issuer;
	private readonly string _audience;

	public JwtService(IConfiguration configuration)
	{
		_key = configuration["Jwt:Key"];
		_issuer = configuration["Jwt:Issuer"];
		_audience = configuration["Jwt:Audience"];
	}

	public string GenerateToken(string userId, string role)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)); // استفاده از کلید 256 بیتی
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(ClaimTypes.Name, userId),
			new Claim(ClaimTypes.Role, role)
		};

		var token = new JwtSecurityToken(
			issuer: _issuer,
			audience: _audience,
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}



