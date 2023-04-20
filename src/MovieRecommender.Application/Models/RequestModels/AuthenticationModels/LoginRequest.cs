using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Application.Models.RequestModels.AuthenticationModels
{
    /// <summary>
    /// Login için Kullanıcı adı veya email adresi kullanılabilir.
    /// Kullanıcı adı veya şifre boş olamaz
    /// </summary>
    public class LoginRequest
    {
        public string UsernameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
