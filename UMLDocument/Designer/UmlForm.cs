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
            umlDesigner1.Uml.Classes.Add(klasse);

            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Id", Primary = true });
            var prop = new Objects.UmlProperty() { Name = "Voornaam" };
            klasse.Properties.Add(prop);
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Achternaam" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Geboortedatum" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Woonplaats" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Gehuwd" });


            prop.MouseClick += delegate { MessageBox.Show("clicked"); };
            prop.MouseEnter += delegate { umlDesigner1.label1.Text = "mouse enter"; };
            prop.MouseLeave += delegate { umlDesigner1.label1.Text = "mouse leave"; };


            var adres = new Objects.UmlClass() { Name = "Adres", Width = 150, Height = 200, X = 400, Y = 100 };
            umlDesigner1.Uml.Classes.Add(adres);
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Id", Primary = true });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Straat" });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Huisnummer" });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Bus" });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Postcode" });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Gemeente" });
            adres.Properties.Add(new Objects.UmlProperty() { Name = "Land" });
        }

        private void tsbAddClass_Click(object sender, EventArgs e)
        {
            var klasse = new Objects.UmlClass() { Name = "Adres", Width = 150, Height = 200, X = 100, Y = 100 };
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Id", Primary = true });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Straat" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Huisnummer" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Postcode" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Gemeente" });
            klasse.Properties.Add(new Objects.UmlProperty() { Name = "Land" });

            umlDesigner1.Uml.Classes.Add(klasse);
        }
    }
}
