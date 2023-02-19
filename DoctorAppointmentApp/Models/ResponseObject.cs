using System.Net;

namespace DoctorAppointmentApp.Models
{
    public class ResponseObject<T>
    {
        public T ReturnObject { get; private set; }

        // Service layer shouldn't be coupled with HTTPs terms (use Exceptions instead).
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; private set; }

        public ResponseObject(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;        }

        public ResponseObject(HttpStatusCode statusCode, string errorMessage)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = errorMessage;
        }

        public ResponseObject(T returnObject, HttpStatusCode statusCode)
        {
            this.ReturnObject = returnObject;
            this.StatusCode = statusCode;
        }

        public ResponseObject(T returnObject, HttpStatusCode statusCode, string errorMessage)
        {
            this.ReturnObject = returnObject;
            this.StatusCode = statusCode;
            this.ErrorMessage = errorMessage;
        }
    }

}

