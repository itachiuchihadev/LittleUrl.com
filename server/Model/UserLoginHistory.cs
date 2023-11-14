using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl.Model
{
    public class UserLoginHistory
    {
        public Guid UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public string ClientIP { get; set; } 
    }
}