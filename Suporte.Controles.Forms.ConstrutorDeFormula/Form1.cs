using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Suporte.Controles.Forms.ConstrutorDeFormula
{
    public partial class Form1 : Form
    {
        int indice = 1;
        public Form1()
        {
            InitializeComponent();
            arrastarESoltar1.ControlesDeDestino.Add(this);
            arrastarESoltar1.AoSoltar += (sender, e) =>
            {
                e.Clonar = true;
            };
            arrastarESoltar1.AoRequisitarClonar += (sender, e) =>
            {
                e.Clonado = (Control)e.Original.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
                Suporte.Controles.Util.ClonarPropriedadesGeraisDoControle(e.Original, e.Clonado);
                ((Label)e.Clonado).BackColor = ((Label)e.Original).BackColor;
                ((Label)e.Clonado).Text = "clone " + (indice++).ToString();
                ((Label)e.Clonado).AutoSize = true;
            };
        }
    }
}
