using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class OtherInformation { 
        public string emailAddress { get; init; } 
        public string homepage { get; init; } 
        public List<int> institutionType { get; init; } 
        public List<OrganizationMapFiles> organizationMapFiles { get; init; } 
        public string organizationMapFilesLastChange { get; init; } 
        public string institutionTypeLastChange { get; init; } 
        public string homepageLastChange { get; init; } 
        public string emailAddressLastChange { get; init; } 
    }
} 
