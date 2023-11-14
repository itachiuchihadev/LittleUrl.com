using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
     public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message, Boolean isauthenticated = true)
        {
            Succeeded = false;
            IsAuthenticated = isauthenticated;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        // public List<ErrorModel> Errors { get; set; }
        public bool IsAuthenticated { get; set; } 
        public T Data { get; set; }
    }
}