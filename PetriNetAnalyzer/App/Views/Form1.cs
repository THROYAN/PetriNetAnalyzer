using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Petri_Net_Editor.App.Views;

namespace PetriNetAnalyzer.App.Views
{
    public partial class Form1 : MarkedPetriGraphEditFormView
    {
        public Form1()
        {
            InitializeComponent();
        }

        public override bool GraphMenuEnable
        {
            set
            {
                base.GraphMenuEnable = value;
                if(menuStrip1.Items["Анализ"] != null)
                    menuStrip1.Items["Анализ"].Enabled = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem item = new ToolStripMenuItem("Анализ") { Name = "Анализ" };
            item.Click += new EventHandler(item_Click);
            this.menuStrip1.Items.Add(item);
            this.GraphMenuEnable = false;
        }

        void item_Click(object sender, EventArgs e)
        {
            var v = new TreeGraphViewForm(this.selectedGraph.petriNet);
            
            v.ShowDialog();
        }
    }
}
