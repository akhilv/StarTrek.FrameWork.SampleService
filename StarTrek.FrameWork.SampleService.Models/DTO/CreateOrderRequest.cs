using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StarTrek.FrameWork.SampleService.Models.DTO
{
   public class CreateOrderRequest
    {
        [Required]
        public string CustomerId { get; set; }
      
        public string Currency { get; set; }

        public decimal? Price { get; set; }

        [IgnoreDataMember]
        public string OrderRef { get; set; }

    }
}
