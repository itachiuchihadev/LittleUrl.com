using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
    public class UserToken
    {
        public  Guid UserTokenId { get; set; }
        public  string UserId { get; set; }
        public  string Token { get; set; }
        [Required]
        public  string JwtToken { get; set; }
        public  DateTime Expires { get; set; }
        public  bool IsExpired => DateTime.UtcNow >= Expires;
        public  string ReplacedByToken { get; set; }
        public  DateTime CreatedDate { get; set; }
        public  DateTime? RevokedDate { get; set; }
        public  string CreatedByIp { get; set; }
        public  string RevokedByIp { get; set; }
        public  bool IsActive => RevokedDate == null && !IsExpired;
        public  User User { get; set; }
    }
   
}