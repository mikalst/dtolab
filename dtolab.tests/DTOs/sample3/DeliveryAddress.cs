using System.Text.Json.Serialization; 

namespace dtolab.tests.sample3.DTOs { 
    public class DeliveryAddress { 
        public int addressId { get; init; } 
        [JsonPropertyName("country")]
        public DeliveryAddressCountry deliveryAddressCountry { get; init; } 
    }
} 
