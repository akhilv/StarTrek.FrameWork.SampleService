namespace StarTrek.FrameWork.SampleService.Models.Exceptions
{
    public enum ErrorCode
    {
        Unspecified = 0,
        ModelBindingException= 500,
        UnhandledException = 1000,
        NotImplementedException = 9000,
    }
}