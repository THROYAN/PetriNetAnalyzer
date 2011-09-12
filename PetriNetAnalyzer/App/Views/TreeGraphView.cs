using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GraphEditor.App.Views;

using MagicLibrary.MathUtils.Graphs.Trees;

using PetriNetAnalyzer.App.Controllers;

namespace PetriNetAnalyzer.App.Views
{
    public class TreeGraphView : GraphView
    {
        public new TreeGraphViewController MainController { get { return this.GetController("MainController") as TreeGraphViewController; } }

        public TreeGraph Tree { get; set; }

        public TreeGraphView(Control control, TreeGraph tree = null)
            : base(control)
        {
            Controllers.Clear();
            Controllers.Add(new TreeGraphViewController(this, "MainController"));

            if (tree != null)
                this.Tree = tree;

            this.Control.MouseWheel += new MouseEventHandler(Control_MouseWheel);

            MainController.ViewLoad();
        }

        void Control_MouseWheel(object sender, MouseEventArgs e)
        {
            this.Scaling += e.Delta / 1200.0;
            this.Refresh();
        }
    }
}
