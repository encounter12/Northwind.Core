using System.Net;

namespace Northwind.Mvc.Helpers
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public T Data { get; set; }
    }
}
