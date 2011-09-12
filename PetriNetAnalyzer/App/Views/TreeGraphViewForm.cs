using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PetriNetAnalyzer.App.Models;

using MagicLibrary.MathUtils.PetriNetsUtils;
using MagicLibrary.MathUtils.PetriNetsUtils.Graphs;

namespace PetriNetAnalyzer.App.Views
{
    public partial class TreeGraphViewForm : Form
    {
        public PetriNet PetriNet { get; set; }

        public TreeGraphView treeGraphView { get; set; }

        public TreeGraphViewForm(PetriNet p)
        {
            InitializeComponent();

            this.PetriNet = p;

            this.treeGraphView = new TreeGraphView(this, this.PetriNet.GetReachabilityTree());
        }

        private void TreeGraphViewForm_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = (this.treeGraphView.graphWrapper.Graph as MarkingTree).GetAnalizeReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] sr = this.textBox1.Text.Split(',');

                if (sr.Length != this.PetriNet.Graph.PlacesCount)
                {
                    throw new Exception();
                }
                //int[] marking = new int[sr.Length];
                //for (int i = 0; i < sr.Length; i++)
                //{
                //    marking[i] = int.Parse(sr[i]);
                //}
                int[] marking = this.textBox1.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim())).ToArray();

                switch((this.treeGraphView.Tree as MarkingTree).IsAchievable(marking))
                {
                    case 1:
                        this.richTextBox1.Text = String.Format("Маркировка {0} достижима.\n\n", (new MarkingTreeNode(null, null, null, marking)).MarkingToString());
                        break;
                    case -1:
                        this.richTextBox1.Text = this.richTextBox1.Text = String.Format("Маркировка {0} недостижима.\n\n", (new MarkingTreeNode(null, null, null, marking)).MarkingToString());;
                        break;
                    case 0:
                        this.richTextBox1.Text = this.richTextBox1.Text = String.Format("О достижиости маркировки {0} по дереву достижимости ничего нельзя сказать.\n\n", (new MarkingTreeNode(null, null, null, marking)).MarkingToString());;
                        break;
                }
            }
            catch
            {
                this.richTextBox1.Text = "***ERROR***\nНеверный формат маркировки\n\n";
            }
            this.richTextBox1.Text += (this.treeGraphView.graphWrapper.Graph as MarkingTree).GetAnalizeReport();
        }
    }
}
