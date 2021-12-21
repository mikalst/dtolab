using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class StandardsAndAgreements { 
        public bool includesAgreements { get; init; } 
        public string agreement { get; init; } 
        public string publicOrganAgreement { get; init; } 
        public List<ApprovalLetter> approvalLetter { get; init; } 
    }
} 
