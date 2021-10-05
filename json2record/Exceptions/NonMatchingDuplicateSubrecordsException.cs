using System;

namespace json2record.Exceptions {
    public class NonMatchingDuplicateSubrecordsException : Exception {
        public NonMatchingDuplicateSubrecordsException(string s) : base(s) {
        }
    }
}