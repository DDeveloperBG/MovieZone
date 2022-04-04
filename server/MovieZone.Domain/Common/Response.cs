namespace MovieZone.Domain.Common
{
    using System.Collections.Generic;

    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            this.Succeeded = true;
            this.Message = message;
            this.Data = data;
        }

        public Response(string message)
        {
            this.Succeeded = false;
            this.Message = message;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }
    }
}
