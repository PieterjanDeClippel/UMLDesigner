using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDesigner.UMLDocument.Enums;

namespace UMLDesigner.UMLDocument.Objects
{
    public class Relation
    {
        public RelationType Type { get; set; }
        public UmlProperty Property1 { get; set; }
        public UmlProperty Property2 { get; set; }
    }
}
