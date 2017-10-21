namespace UMLDesigner.UMLDocument.Designer
{
    partial class UmlDesigner
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // UmlDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "UmlDesigner";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UmlDesigner_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UmlDesigner_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UmlDesigner_MouseDown);
            this.MouseEnter += new System.EventHandler(this.UmlDesigner_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UmlDesigner_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UmlDesigner_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UmlDesigner_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
    }
}
