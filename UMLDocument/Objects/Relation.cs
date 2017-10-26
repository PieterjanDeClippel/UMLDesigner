using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Draw(Graphics graphics, Point p1, Point p2)
        {
            Point[] ptn = new Point[]
            {
                p1,
                new Point(p2.X - p1.X / 2, p1.Y),
                new Point(p2.X - p1.X / 2, p2.Y),
                p2
            };
            graphics.DrawLines(Pens.Black, ptn);
        }
    }
}
