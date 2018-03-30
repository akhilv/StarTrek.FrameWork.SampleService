using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization;

namespace StarTrek.FrameWork.SampleService.Models.Exceptions
{
    [Serializable]
    public class HttpStatusCodeException : Exception
    {
        public ErrorResponse Error { get; }

        public HttpStatusCode StatusCode { get; }

        public HttpStatusCodeException()
        {
            StatusCode = HttpStatusCode.InternalServerError;
            Error = new ErrorResponse(ErrorCode.Unspecified, string.Empty);
        }

        public HttpStatusCodeException(string message) : this(HttpStatusCode.InternalServerError, message)
        {

        }

        public HttpStatusCodeException(string message, Exception ex) : this(HttpStatusCode.InternalServerError, message,
            ex)
        {

        }

        public HttpStatusCodeException(HttpStatusCode statusCode, ErrorResponse error)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            Error = new ErrorResponse(ErrorCode.Unspecified, message);
        }

        //public HttpStatusCodeException(HttpStatusCode statusCode, string message, CustomException ex) : base(message, ex)
        //{
        //    StatusCode = statusCode;
        //    if (ex != null)
        //        Error = new ErrorResponse(ex.ErrorCode, message);
        //}

        public HttpStatusCodeException(HttpStatusCode statusCode, string message, Exception ex) : base(message, ex)
        {
            StatusCode = statusCode;
            Error = new ErrorResponse(ErrorCode.Unspecified, message);
        }

        protected HttpStatusCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StatusCode = (HttpStatusCode) info.GetValue("StatusCode", typeof(HttpStatusCode));
            Error = (ErrorResponse) info.GetValue("Error", typeof(ErrorResponse));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info?.AddValue("StatusCode", StatusCode, typeof(HttpStatusCode));
            info?.AddValue("Error", Error, typeof(ErrorResponse));
        }
    }
}
