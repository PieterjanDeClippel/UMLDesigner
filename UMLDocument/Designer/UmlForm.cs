using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLDesigner.UMLDocument.Designer
{
    public partial class UmlForm : Form
    {
        public UmlForm()
        {
            InitializeComponent();

            umlDesigner1.Uml = new Objects.UML();

            var klasse = new Objects.UmlClass() { Name = "Persoon", Width = 150, Height = 200, X = 100, Y = 100 };

            var prop = new Objects.UmlProperty() { Name = "Voornaam" };
            klasse.Properties.Add(prop);
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Achternaam" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Geboortedatum" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Woonplaats" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Gehuwd" });


            prop.MouseClick += delegate { MessageBox.Show("clicked"); };
            prop.MouseEnter += delegate { umlDesigner1.label1.Text = "mouse enter"; };
            prop.MouseLeave += delegate { umlDesigner1.label1.Text = "mouse leave"; };

            umlDesigner1.Uml.Classes.Add(klasse);
        }
    }
}
