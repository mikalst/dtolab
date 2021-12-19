using System;

namespace json2record.common.Exceptions {
    public class NonMatchingDuplicateSubrecordsException : Exception {
        public NonMatchingDuplicateSubrecordsException(string s) : base(s) {
        }
    }
}