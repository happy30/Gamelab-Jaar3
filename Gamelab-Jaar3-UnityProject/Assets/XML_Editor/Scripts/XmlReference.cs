
using System;

namespace XmlEditor {
    [AttributeUsage(AttributeTargets.Field)]
    public class XmlReference : Attribute {

        string reference;
        
        public XmlReference(string displayName) {
            reference = displayName;
        }

        public override string ToString() {
            return reference;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class XmlTypeReference : Attribute {

        public Type reference;

        public XmlTypeReference(Type typeName) {
            reference = typeName;
        }

        public override string ToString() {
            return reference.ToString();
        }
    }
}
