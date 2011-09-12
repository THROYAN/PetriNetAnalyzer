using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GraphEditor.App.Controllers;

using PetriNetAnalyzer.App.Views;
using PetriNetAnalyzer.App.Models;

using MagicLibrary.MathUtils.PetriNetsUtils.Graphs;

namespace PetriNetAnalyzer.App.Controllers
{
    public class TreeGraphViewController : GraphEditController
    {
        public TreeGraphView tgv { get { return this.View as TreeGraphView; } }

        public TreeGraphViewController(TreeGraphView view, string name)
            : base(view, name)
        {

        }

        public new void ViewLoad()
        {
            tgv.action = GraphEditor.App.Views.GraphEditActions.SomethingElse;
            tgv.graphWrapper = new MarkingTreeWrapper(tgv.Tree as MarkingTree)
            {
                RootPosition = new System.Drawing.PointF(tgv.Control.Width / 2, 50)
            };

            (tgv.graphWrapper as TreeGraphWrapper).ResetAllNodesPositions(tgv.Control.CreateGraphics());
        }
    }
}
