using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class VocationalCompanyQualityAssurance { 
        public bool hasNokutPreviouslyReceivedQualityAssurance { get; init; } 
        public string changesInSystemDescription { get; init; } 
        public string dateForApproval { get; init; } 
        public List<SystemDescriptionFiles> systemDescriptionFiles { get; init; } 
        public List<YearReportFiles> yearReportFiles { get; init; } 
        public List<TemplateEvaluationFormFiles> templateEvaluationFormFiles { get; init; } 
        public List<OtherDocumentFiles> otherDocumentFiles { get; init; } 
    }
} 
