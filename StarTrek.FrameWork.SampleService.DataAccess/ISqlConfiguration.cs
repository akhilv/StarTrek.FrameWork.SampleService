namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public interface ISqlConfiguration
    {
        string ConnectionString { get; set; }

        int CommandTimeOut { get; set; }
    }
}