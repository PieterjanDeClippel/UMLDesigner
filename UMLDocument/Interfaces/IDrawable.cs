using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDesigner.UMLDocument.Interfaces
{
    public interface IDrawable
    {
        bool GetHitTest(Designer.UmlDesigner control, Point pos);
        void Draw(Graphics gr, Font font, Color textColor);
    }
}
