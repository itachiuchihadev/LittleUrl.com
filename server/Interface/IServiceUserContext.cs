using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
    public interface IServiceUserContext
    {
        public string UserName { get; }
        public Guid UserId { get; }
        public string RoleName { get; }
        public bool IsAuthenticated { get; }
    }
}