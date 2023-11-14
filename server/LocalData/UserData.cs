using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LittleUrl;

namespace LittleUrl
{
    public static class UserData
    {
        public static IEnumerable<User> GetUsersData()
        {
            var filepath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "LocalData/users.json");
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(File.ReadAllText(filepath));
            foreach(var user in users)
            {
                user.Password = DateTime.Now.Date.ToString("ddMMyyyy");
            }
            return users;
        }
    }
}