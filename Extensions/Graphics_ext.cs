using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDesigner.Extensions
{
    public static class Graphics_ext
    {
        public static void DrawFillRectangle(this Graphics gr, Pen pen, Brush brush, int x, int y, int width, int height)
        {
            gr.FillRectangle(brush, x, y, width, height);
            gr.DrawRectangle(pen, x, y, width, height);
        }
    }
}
