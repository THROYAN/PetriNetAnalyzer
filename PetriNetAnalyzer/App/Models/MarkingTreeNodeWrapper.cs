using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using GraphEditor.App.Models;

using MagicLibrary.MathUtils;
using MagicLibrary.MathUtils.PetriNetsUtils.Graphs;

namespace PetriNetAnalyzer.App.Models
{
    public class MarkingTreeNodeWrapper : TreeNodeWrapper
    {
        public new MarkingTreeNode Node { get { return this.Vertex as MarkingTreeNode; } }

        public MarkingTreeNodeWrapper(MarkingTreeWrapper wrapper, MarkingTreeNode node)
            : base(wrapper, node)
        {

        }

        public override string Text
        {
            get
            {
                return this.Node.MarkingToString();
            }
        }
    }
}
