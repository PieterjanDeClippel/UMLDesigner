using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDesigner.Shared;

namespace UMLDesigner.UMLDocument.Objects
{
    public class UML
    {
        public UML()
        {
            Classes = new Collectie<UmlClass>();
            Relations = new Collectie<Relation>();

            Classes.Changed += Classes_Changed;
        }

        private void Classes_Changed(object sender, Collectie<UmlClass>.CollectieChangedEventArgs e)
        {
            foreach (var cls in e.ItemsToegevoegd)
                cls.Properties.Changed += Properties_Changed;
            foreach (var cls in e.ItemsVerwijderd)
                cls.Properties.Changed -= Properties_Changed;
        }

        private void Properties_Changed(object sender, Collectie<UmlProperty>.CollectieChangedEventArgs e)
        {
            foreach (var property in e.ItemsToegevoegd)
            {
                property.MouseDown += Property_MouseDown;
                property.MouseUp += Property_MouseUp;
            }
            foreach (var property in e.ItemsVerwijderd)
            {
                property.MouseDown -= Property_MouseDown;
                property.MouseUp -= Property_MouseUp;
            }
        }

        private void Property_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            NewRelation = new Relation() { Property1 = (UmlProperty)sender };
        }

        private void Property_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(NewRelation != null)
            {
                NewRelation.Property2 = (UmlProperty)sender;
                Relations.Add(NewRelation);
                NewRelation = null;
            }
        }

        public Collectie<UmlClass> Classes { get; set; }
        public Collectie<Relation> Relations { get; set; }
        private Relation NewRelation;
    }
}
