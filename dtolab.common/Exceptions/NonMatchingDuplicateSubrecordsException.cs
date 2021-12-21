using System;

namespace dtolab.common.Exceptions {
    public class NonMatchingDuplicateSubrecordsException : Exception {
        public NonMatchingDuplicateSubrecordsException(string s) : base(s) {
        }
    }
}