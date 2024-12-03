using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Securities;
public class PasswordManagment
{
	public static HashPasswordDto HashingPassword(string password)
	{
		using (var hmac = new System.Security.Cryptography.HMACSHA512())
		{
			var saltPass = hmac.Key;
			var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return new HashPasswordDto
			{
				Password = passwordHash,
				SaltPassword = saltPass
			};
		}
	}

	public static bool VerifyPassword(string password, byte[] hashPassword, byte[] saltPassword)
	{
		using (var hmac = new System.Security.Cryptography.HMACSHA512(saltPassword))
		{

			var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			for (var i = 0; i < ComputeHash.Length; i++)
			{
				if (ComputeHash[i] != hashPassword[i])
					return false;
			}
			return true;
		}
	}
}
