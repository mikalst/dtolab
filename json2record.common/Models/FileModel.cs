using System;
using System.Collections.Generic;
using System.Linq;

namespace json2record.common {
    public class FileModel {
        public string name { get; init; }
        public HashSet<AttributeModel> attributes { get; init; }
        public HashSet<string> packages { get; init; } 

        public override bool Equals(object o) => this.Equals((FileModel)o);

        public bool Equals(FileModel rhs) {
            if (this.name != rhs.name) return false;

            var commonAttributes = attributes.Union(rhs.attributes);
            if (commonAttributes.Count() != attributes.Count() || commonAttributes.Count() != rhs.attributes.Count()) return false;
            var commonPackages = packages.Union(rhs.packages);
            if (commonPackages.Count() != packages.Count() || commonPackages.Count() != rhs.packages.Count()) return false;
            
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