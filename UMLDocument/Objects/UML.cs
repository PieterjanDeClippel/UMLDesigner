using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDesigner.UMLDocument.Objects
{
    public class UML
    {
        public UML()
        {
            Classes = new List<UmlClass>();
            Relations = new List<Relation>();
        }

        public List<UmlClass> Classes { get; set; }
        public List<Relation> Relations { get; set; }
    }
}
