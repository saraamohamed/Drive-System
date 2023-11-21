using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task_Codid.ViewModels
{
    [Authorize]

    public class loginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
