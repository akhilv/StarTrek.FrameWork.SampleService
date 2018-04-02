namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public class SqlConfiguration : ISqlConfiguration
    {
        public string ConnectionString { get; set; }
        public int CommandTimeOut { get; set; }
    }
}