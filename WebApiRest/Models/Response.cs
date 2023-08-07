using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiRest.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        public Response(HttpStatusCode status, string message, Object data)
        {
            StatusCode = status;
            Message = message;
            Data = data;
        }
    }
}