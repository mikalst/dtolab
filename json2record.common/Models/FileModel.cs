using System;
using System.Collections.Generic;

namespace json2record.common {
    public struct FileModel : IEquatable<FileModel> {
        public string name { get; init; }
        public List<AttributeModel> attributes { get; init; }
        public HashSet<string> packages { get; init; } 

        public override bool Equals(object o) => this.Equals((FileModel)o);

        public bool Equals(FileModel rhs) {
            if (this.name != rhs.name) return false;

            foreach(AttributeModel a in attributes) {
                if (!(rhs.attributes.Contains(a))) return false;
            }
            foreach(string package in packages) {
                if (!(rhs.packages.Contains(package))) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(FileModel lhs, FileModel rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(FileModel lhs, FileModel rhs)
        {
            return !lhs.Equals(rhs);
        }
    }

}