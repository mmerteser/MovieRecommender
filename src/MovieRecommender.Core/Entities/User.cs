using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstLastName { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }

        public virtual IEnumerable<MovieRating> MovieRatings { get; set; }
    }
}
