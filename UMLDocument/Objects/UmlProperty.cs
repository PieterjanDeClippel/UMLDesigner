using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLDesigner.UMLDocument.Objects
{
    public class UmlProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseMove;

        private bool is_hover = false;

        public void Draw(Graphics gr, Font font, Color textColor, int x, int y, int width)
        {
            Font item_font = new Font(font, FontStyle.Regular);

            if (is_hover)
                gr.FillRectangle(new SolidBrush(Color.FromArgb(220, 220, 220)), x, y, width, 22);
            gr.DrawString(Name, item_font, new SolidBrush(textColor), new RectangleF(x + 10 + 16, y, width - 30, 22), new StringFormat() { LineAlignment = StringAlignment.Center });
            gr.DrawImage(Properties.Resources.property, new Rectangle(x + 10, y + 4, 14, 14));
        }

        public void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
                MouseClick(this, e);
        }
        public void OnMouseEnter(MouseEventArgs e)
        {
            is_hover = true;
            if (MouseEnter != null)
                MouseEnter(this, e);
        }
        public void OnMouseLeave(EventArgs e)
        {
            is_hover = false;
            if (MouseLeave != null)
                MouseLeave(this, e);
        }
        public void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
        }
    }
}
