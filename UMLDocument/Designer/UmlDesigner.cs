using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLDesigner.UMLDocument.Objects;
using System.Drawing.Drawing2D;

namespace UMLDesigner.UMLDocument.Designer
{
    public partial class UmlDesigner : UserControl
    {
        public UmlDesigner()
        {
            InitializeComponent();
        }

        public UML Uml { get; set; }

        private void UmlDesigner_Paint(object sender, PaintEventArgs e)
        {
            if (Uml != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (var cls in Uml.Classes)
                    cls.Draw(e.Graphics, Font, ForeColor);
            }
        }

        #region Helper Functions
        private UmlClass GetObjectBelowCursor(Point pos)
        {
            if (Uml != null)
            {
                var classes = Uml.Classes.ToArray(); classes.Reverse();
                foreach (var cls in classes)
                    if (cls.GetHitTest(this, pos))
                        return cls;
            }
            return null;
        }
        #endregion
        #region Drag'n'Drop
        private UmlClass move_object = null;
        private Point grip_point = new Point();
        private void UmlDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (hovered_uml == null)
                {
                    move_object = hovered_uml;
                    grip_point = new Point(e.Location.X - move_object.X, e.Location.Y - move_object.Y);
                }
                else if (hovered_uml.IsMouseInHeader)
                {
                    move_object = hovered_uml;
                    grip_point = new Point(e.Location.X - move_object.X, e.Location.Y - move_object.Y);
                }
                else
                    move_object = null;
            }
        }
        private void UmlDesigner_MouseUp(object sender, MouseEventArgs e)
        {
            move_object = null;
        }
        #endregion
        #region Pass Events to children

        private UmlClass hovered_uml;
        private void UmlDesigner_MouseClick(object sender, MouseEventArgs e)
        {
            var cls = GetObjectBelowCursor(e.Location);
            if(cls != null)
                cls.OnMouseClick(new MouseEventArgs(e.Button, e.Clicks, e.X - cls.X, e.Y - cls.Y, e.Delta));
        }
        private void UmlDesigner_MouseEnter(object sender, EventArgs e)
        {
            // get hovered object
            var loc = PointToClient(MousePosition);
            hovered_uml = GetObjectBelowCursor(loc);

            // fire the MouseEnter event
            if (hovered_uml != null)
                hovered_uml.OnMouseEnter(new MouseEventArgs(MouseButtons.None, 0, loc.X - hovered_uml.X, loc.Y - hovered_uml.Y, 0));

            // redraw
            Invalidate();
        }
        private void UmlDesigner_MouseLeave(object sender, EventArgs e)
        {
            if (hovered_uml != null)
            {
                hovered_uml.OnMouseLeave(EventArgs.Empty);
                hovered_uml = null;
            }
        }
        private void UmlDesigner_MouseMove(object sender, MouseEventArgs e)
        {
            if (move_object != null)
            {
                move_object.X = e.X - grip_point.X;
                move_object.Y = e.Y - grip_point.Y;
            }
            else
            {
                var cls = GetObjectBelowCursor(e.Location);
                if (cls == null)
                {
                    if (hovered_uml != null)
                    {
                        //hovered_uml.OnMouseMove(EventArgs.Empty);
                        hovered_uml.OnMouseLeave(EventArgs.Empty);
                        hovered_uml = null;
                    }
                }
                else if (ReferenceEquals(cls, hovered_uml))
                {
                    hovered_uml.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, e.Location.X - hovered_uml.X, e.Location.Y - hovered_uml.Y, 0));
                }
                else
                {
                    if (hovered_uml != null)
                    {
                        hovered_uml.OnMouseLeave(EventArgs.Empty);
                    }
                    cls.OnMouseEnter(new MouseEventArgs(MouseButtons.None, 0, e.Location.X - cls.X, e.Location.Y - cls.Y, 0));
                    hovered_uml = cls;
                }

                if (cls == null)
                    Cursor = Cursors.Default;
                else if (cls.IsMouseInHeader)
                    Cursor = Cursors.SizeAll;
                else
                    Cursor = Cursors.Default;
            }
            Invalidate();
        }
        #endregion
    }
}
