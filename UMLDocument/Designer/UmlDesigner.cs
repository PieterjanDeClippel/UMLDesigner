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
using UMLDesigner.UMLDocument.Interfaces;
using UMLDesigner.Extensions;

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

                // draw the objects
                foreach (var cls in Uml.Classes)
                    cls.Draw(e.Graphics, Font, ForeColor);
                foreach (var rel in Uml.Relations)
                    rel.Draw(e.Graphics, rel.Property1, rel.Property2);

                const int size = 5;

                //draw the glyphs
                foreach (var cls in Uml.Classes)
                    if(cls.GetType().GetInterfaces().Contains(typeof(IRectangular)))
                    {
                        e.Graphics.DrawFillRectangle(Pens.Black, Brushes.Lime, cls.X - size, cls.Y - size, size*2, size * 2);
                        e.Graphics.DrawFillRectangle(Pens.Black, Brushes.Lime, cls.X + cls.Width - size, cls.Y - size, size * 2, size * 2);
                        e.Graphics.DrawFillRectangle(Pens.Black, Brushes.Lime, cls.X + cls.Width - size, cls.Y + cls.Height - size, size * 2, size * 2);
                        e.Graphics.DrawFillRectangle(Pens.Black, Brushes.Lime, cls.X - size, cls.Y + cls.Height - size, size * 2, size * 2);

                    }
            }
        }

        #region Actions
        public enum eAction
        {
            None,
            Move_Class
        }
        public eAction Action { get; private set; } = eAction.None;
        #endregion
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
            if (hovered_uml == null)
            { }
            else if (hovered_uml.IsMouseInHeader)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // System action
                    move_object = hovered_uml;
                    grip_point = new Point(e.Location.X - move_object.X, e.Location.Y - move_object.Y);
                    Action = eAction.Move_Class;
                }
                else
                {
                    // No system action
                    // Reroute the event to the UmlClass
                    var cls = GetObjectBelowCursor(e.Location);
                    if (cls != null)
                        cls.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, e.X - cls.X, e.Y - cls.Y, e.Delta));
                }
            }
            else
            {
                Action = eAction.None;
                move_object = null;
                // No system action
                // Reroute the event to the UmlClass
                hovered_uml.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, e.X - hovered_uml.X, e.Y - hovered_uml.Y, e.Delta));
            }
        }
        private void UmlDesigner_MouseUp(object sender, MouseEventArgs e)
        {
            if(move_object != null)
            {
                // System action
                // We were dragging a UmlClass
                move_object = null;
            }
            else
            {
                // No system action
                // Reroute the event to the UmlClass
                var cls = GetObjectBelowCursor(e.Location);
                if (cls != null)
                    cls.OnMouseUp(new MouseEventArgs(e.Button, e.Clicks, e.X - cls.X, e.Y - cls.Y, e.Delta));
            }
        }
        #endregion
        #region Pass Events to children

        private UmlClass hovered_uml;
        private void UmlDesigner_MouseClick(object sender, MouseEventArgs e)
        {
            if (false)
            {
                // System action
                // We don't need this
            }
            else
            {
                // No system action
                // Reroute the event to the UmlClass
                var cls = GetObjectBelowCursor(e.Location);
                if (cls != null)
                    cls.OnMouseClick(new MouseEventArgs(e.Button, e.Clicks, e.X - cls.X, e.Y - cls.Y, e.Delta));
            }
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
                // System action
                // Move the dragged object
                move_object.X = e.X - grip_point.X;
                move_object.Y = e.Y - grip_point.Y;
            }
            else
            {
                // No system action
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
