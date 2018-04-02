using System.ComponentModel.DataAnnotations;

namespace StarTrek.FrameWork.SampleService.Models.DTO
{
    public class UpdateOrderRequest
    {
        [Required]
        public string OrderRef { get; set; }

        [Required]
        public string OrderId { get; set; }

        public string Currency { get; set; }

    }
}