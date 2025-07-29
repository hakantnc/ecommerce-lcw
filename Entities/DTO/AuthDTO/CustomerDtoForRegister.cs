using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.AuthDTO
{
    public record CustomerDtoForRegister
    {
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = null!;
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]

        [MaxLength(150, ErrorMessage = "Şifre 150 karakteri geçemez.")]
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
