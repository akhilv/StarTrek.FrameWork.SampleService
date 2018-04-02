namespace StarTrek.FrameWork.SampleService.Models.Exceptions
{
    public enum ErrorCode
    {
        Unspecified = 0,
        ModelBindingException= 500,
        SQLException = 1000,
        UnhandledException = 9000
    }
}