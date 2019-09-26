namespace Suporte.Controles.Forms.ConstrutorDeFormula
{
    partial class Form1
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
            this.arrastarESoltar1 = new Suporte.Controles.Forms.ArrastarESoltar();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.organizarControles1 = new Suporte.Controles.Forms.OrganizarControles();
            this.SuspendLayout();
            // 
            // arrastarESoltar1
            // 
            this.arrastarESoltar1.Ativado = true;
            this.arrastarESoltar1.Controle = this.label1;
            this.arrastarESoltar1.ControleDeDestino = this.panel1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(12, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 182);
            this.panel1.TabIndex = 1;
            // 
            // organizarControles1
            // 
            this.organizarControles1.Ativado = true;
            this.organizarControles1.Controle = this.panel1;
            this.organizarControles1.EspacamentoEntreControles = new System.Windows.Forms.Padding(5);
            this.organizarControles1.IntegracaoComArrastarESoltar = true;
            this.organizarControles1.MargemDoControle = new System.Windows.Forms.Padding(5);
            this.organizarControles1.ModoDeExibicao = Suporte.Controles.Forms.OrganizarControles.ModosDeExibicao.LadoALado;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(474, 326);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ArrastarESoltar arrastarESoltar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private OrganizarControles organizarControles1;


    }
}