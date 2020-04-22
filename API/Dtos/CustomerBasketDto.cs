using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } // Will be initialized in the Client side

        public List<BasketItemDto> Items { get; set; }
    }
}