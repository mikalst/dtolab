using System.Collections.Generic; 

namespace dtolab.tests.sample6.DTOs { 
    public class BoardMemberSetup { 
        public List<string> boardMembers { get; init; } 
        public string commentToBoardMembers { get; init; } 
        public string boardSetupComment { get; init; } 
        public List<Files> files { get; init; } 
        public string boardMembersLastChange { get; init; } 
        public string commentToBoardMembersLastChange { get; init; } 
        public string boardSetupCommentLastChange { get; init; } 
        public string filesLastChange { get; init; } 
    }
} 
