using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StarTrek.FrameWork.SampleService.Models
{
   public class CreateOrderRequest
    {
        [Required]
        public string CustomerId { get; set; }

        public string OrderRef { get; set; }

        [Required]
        public string OrderId { get; set; }
        public string Currency { get; set; }

        public decimal Price { get; set; }

    }
}
