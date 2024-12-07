using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.Account.Dtos
{
	public record RegisterDto
	{
		[Required(ErrorMessage = "Username is required.")]
		[StringLength(50, ErrorMessage = "Username must be less than 50 characters.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		[StringLength(200, ErrorMessage = "Email must be less than 200 characters.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
		[DataType(DataType.Password)]
		public string StrPassword { get; set; }

		[Required(ErrorMessage = "Please confirm your password.")]
		[Compare("StrPassword", ErrorMessage = "Passwords do not match.")]
		public string RePassword { get; set; }
	}

}
