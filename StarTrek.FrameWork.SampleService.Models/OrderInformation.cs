using System;

namespace StarTrek.FrameWork.SampleService.Models
{
    public class OrderInformation
    {
        public string OrderRef { get; set; }

        public int OrderId { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset ModifiedDateTime { get; set; }

        public string Currency { get; set; }

        public string CustomerId { get; set; }

        public decimal? Price { get; set; }
    }
}
