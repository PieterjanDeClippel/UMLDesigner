namespace UMLDesigner.UMLDocument.Designer
{
    partial class UmlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UmlForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddClass = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.umlDesigner1 = new UMLDesigner.UMLDocument.Designer.UmlDesigner();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddClass,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(615, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddClass
            // 
            this.tsbAddClass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddClass.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddClass.Image")));
            this.tsbAddClass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddClass.Name = "tsbAddClass";
            this.tsbAddClass.Size = new System.Drawing.Size(24, 24);
            this.tsbAddClass.Text = "Add Class";
            this.tsbAddClass.Click += new System.EventHandler(this.tsbAddClass_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // umlDesigner1
            // 
            this.umlDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.umlDesigner1.Location = new System.Drawing.Point(0, 27);
            this.umlDesigner1.Name = "umlDesigner1";
            this.umlDesigner1.Size = new System.Drawing.Size(615, 380);
            this.umlDesigner1.TabIndex = 1;
            this.umlDesigner1.Uml = null;
            // 
            // UmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 407);
            this.Controls.Add(this.umlDesigner1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UmlForm";
            this.Text = "UmlForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddClass;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private UmlDesigner umlDesigner1;
    }
}