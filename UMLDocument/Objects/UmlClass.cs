using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLDesigner.UMLDocument.Interfaces;

namespace UMLDesigner.UMLDocument.Objects
{
    public class UmlClass : IDrawable
    {
        #region Constructor
        public UmlClass()
        {
            Properties = new List<UmlProperty>();
        }
        #endregion
        #region Properties
        public string Name { get; set; }
        public List<UmlProperty> Properties { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        #endregion
        #region Constants
        private const int corner_radius = 15;
        private const int heading_height = 40;
        #endregion
        #region Calculated shapes
        private GraphicsPath CalculateOutline()
        {
            GraphicsPath outline = new GraphicsPath();
            outline.AddLine(X + corner_radius, Y, X + Width - corner_radius, Y);
            outline.AddArc(X + Width - 2 * corner_radius, Y, 2 * corner_radius, 2 * corner_radius, -90, 90);
            outline.AddLine(X + Width, Y + corner_radius, X + Width, Y + Height - corner_radius);
            outline.AddArc(X + Width - 2 * corner_radius, Y + Height - 2 * corner_radius, 2 * corner_radius, 2 * corner_radius, 0, 90);
            outline.AddLine(X + Width - corner_radius, Y + Height, X + corner_radius, Y + Height);
            outline.AddArc(X, Y + Height - 2 * corner_radius, 2 * corner_radius, 2 * corner_radius, 90, 90);
            outline.AddLine(X, Y + Height - corner_radius, X, Y + corner_radius);
            outline.AddArc(X, Y, 2 * corner_radius, 2 * corner_radius, 180, 90);
            return outline;
        }
        private GraphicsPath CalculateShadow(GraphicsPath outline)
        {
            GraphicsPath shadow = (GraphicsPath)outline.Clone();
            Matrix translate = new Matrix();
            translate.Translate(3, 3);
            shadow.Transform(translate);
            return shadow;
        }
        private GraphicsPath CalculateHeading()
        {
            GraphicsPath heading = new GraphicsPath();
            heading.AddLine(X + corner_radius, Y, X + Width - corner_radius, Y);
            heading.AddArc(X + Width - 2 * corner_radius, Y, 2 * corner_radius, 2 * corner_radius, -90, 90);
            heading.AddLine(X + Width, Y + corner_radius, X + Width, Y + heading_height);
            heading.AddLine(X + Width, Y + heading_height, X, Y + heading_height);
            heading.AddLine(X, Y + heading_height, X, Y + corner_radius);
            heading.AddArc(X, Y, 2 * corner_radius, 2 * corner_radius, 180, 90);
            return heading;
        }
        #endregion
        #region Helper Functions
        private UmlProperty GetPropertyBelowCursor(Point pos)
        {
            int index = (pos.Y - heading_height - 22) / 22;
            if (pos.Y - heading_height - 22 < 0)
                return null;
            else if ((0 <= index) & (index < Properties.Count))
                return Properties[index];
            else
                return null;
        }
        #endregion

        public bool GetHitTest(Designer.UmlDesigner control, Point pos)
        {
            Bitmap bmp = new Bitmap(control.Width, control.Height);
            Graphics gr = Graphics.FromImage(bmp);
            gr.FillPath(Brushes.Black, CalculateOutline());
            return (bmp.GetPixel(pos.X, pos.Y).ToArgb() == Color.Black.ToArgb());
        }

        public void Draw(Graphics gr, Font font, Color textColor)
        {
            GraphicsPath outline = CalculateOutline();
            GraphicsPath shadow = CalculateShadow(outline);
            GraphicsPath heading = CalculateHeading();
            Rectangle subheading = new Rectangle(X, Y + heading_height, Width, 22);

            Pen outline_pen = new Pen(Color.Black, 1f);
            Brush shadow_brush = new SolidBrush(is_hover ? Color.FromArgb(108, 108, 108) : Color.FromArgb(180, 180, 180));
            LinearGradientBrush heading_br = new LinearGradientBrush(
                new Point(X, 0),
                new Point(X + Width, 0),
                Color.FromArgb(211, 220, 239),
                Color.FromArgb(255, 255, 255));
            Font heading_font = new Font(font, FontStyle.Bold);
            Font subheading_font = new Font(font, FontStyle.Regular);
            Brush subheading_br = new SolidBrush(Color.FromArgb(240, 242, 249));

            gr.FillPath(shadow_brush, shadow);
            gr.FillPath(Brushes.White, outline);
            gr.FillPath(heading_br, heading);
            gr.FillRectangle(subheading_br, subheading);
            gr.DrawString(Name, heading_font, new SolidBrush(textColor), new RectangleF(X + 10, Y, Width, heading_height), new StringFormat() { LineAlignment = StringAlignment.Center });
            gr.DrawString("Properties", subheading_font, new SolidBrush(textColor), new RectangleF(X + 10, Y + heading_height, Width - 20, 22), new StringFormat() { LineAlignment = StringAlignment.Center });
            gr.DrawPath(outline_pen, outline);

            int y = Y + heading_height + 22;
            foreach (var property in Properties)
            {
                property.Draw(gr, font, textColor, X, y, Width);
                y += 22;
            }
        }

        public event MouseEventHandler MouseClick;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event EventHandler MouseMove;

        private bool is_hover = false;
        private UmlProperty hovered_property;

        private bool is_mouse_in_header = false;
        public bool IsMouseInHeader
        {
            get { return is_mouse_in_header; }
            private set { is_mouse_in_header = value; }
        }

        public void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
                MouseClick(this, e);

            var prop = GetPropertyBelowCursor(e.Location);
            if (prop != null)
                prop.OnMouseClick(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y - 22 - heading_height, e.Delta));
        }
        public void OnMouseEnter(MouseEventArgs e)
        {
            // e.location = position relative to this object
            is_hover = true;
            hovered_property = GetPropertyBelowCursor(e.Location);

            if (MouseEnter != null)
                MouseEnter(this, e);

            // fire the MouseEnter event
            if (hovered_property != null)
                hovered_property.OnMouseEnter(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y - 22 - heading_height, e.Delta));

        }
        public void OnMouseLeave(EventArgs e)
        {
            // e.location = position relative to this object
            is_hover = false;
            var old_property = hovered_property;
            hovered_property = null;

            if (MouseLeave != null)
                MouseLeave(this, e);

            if (old_property != null)
                old_property.OnMouseLeave(e);
        }
        public void OnMouseMove(MouseEventArgs e)
        {
            // e.location = position relative to this object

            if((0 < e.Location.X) & (e.Location.X < Width) &
               (0 < e.Location.Y) & (e.Location.Y < heading_height))
            {
                is_mouse_in_header = true;
            }
            else { is_mouse_in_header = false; }

            var prop = GetPropertyBelowCursor(e.Location);
            if(prop == null)
            {
                if(hovered_property != null)
                {
                    hovered_property.OnMouseLeave(EventArgs.Empty);
                    hovered_property = null;
                }
            }
            else if(ReferenceEquals(prop, hovered_property))
            {
                hovered_property.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, e.Location.X, e.Location.Y - 22 - heading_height, 0));
            }
            else
            {
                if(hovered_property != null)
                {
                    hovered_property.OnMouseLeave(EventArgs.Empty);
                }

                prop.OnMouseEnter(new MouseEventArgs(MouseButtons.None, 0, e.Location.X - 0, e.Location.Y - 22 - heading_height, 0));
                hovered_property = prop;
            }
            if (MouseMove != null)
                MouseMove(this, e);
        }

    }
}
