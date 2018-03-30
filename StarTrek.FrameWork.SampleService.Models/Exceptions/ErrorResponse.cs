using System.Runtime.Serialization;

namespace StarTrek.FrameWork.SampleService.Models.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(ErrorCode code, string message)
        {
            ErrorCode = code;
            Message = message;
        }

        [DataMember(Order = 1)]
        public ErrorCode ErrorCode { get;  set; }

        [DataMember(Order = 2)]
        public string Message { get;  set; }
    }
}
