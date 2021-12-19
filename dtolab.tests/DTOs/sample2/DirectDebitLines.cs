using System; 

namespace dtolab.tests.sample2.DTOs { 
    public class DirectDebitLines { 
        public string id { get; init; } 
        public string mandateId { get; init; } 
        public string mandateDescription { get; init; } 
        public DateTime dateOfSignature { get; init; } 
        public bool isDefault { get; init; } 
        public bool oneTime { get; init; } 
        public string bic { get; init; } 
        public DateTime lastCollectionDate { get; init; } 
        public double maxAmount { get; init; } 
        public DateTime expirationDate { get; init; } 
    }
} 
