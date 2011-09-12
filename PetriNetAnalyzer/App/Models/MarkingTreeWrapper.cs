using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils.PetriNetsUtils.Graphs;

namespace PetriNetAnalyzer.App.Models
{
    public class MarkingTreeWrapper : TreeGraphWrapper
    {
        public new MarkingTree Tree { get { return this.Graph as MarkingTree; } }

        public MarkingTreeWrapper(MarkingTree tree)
            : base(tree)
        {
            this.VertexWrappers.Clear();
            this.ArcWrappers.Clear();

            this.Tree.GetVertices().ForEach(v => this.VertexWrappers.Add(new MarkingTreeNodeWrapper(this, v as MarkingTreeNode)));
            this.Tree.GetEdges().ForEach(e => this.ArcWrappers.Add(new NamedArcWrapper(this, e as NamedArc)));

            MarkingTreeWrapper.SetDefaultEventHandlers(this);
        }

        public static void SetDefaultEventHandlers(MarkingTreeWrapper wrapper)
        {
            TreeGraphWrapper.SetDefaultEventHandlers(wrapper);

            wrapper.Tree.OnVertexAdded += new EventHandler<MagicLibrary.MathUtils.Graphs.VerticesModifiedEventArgs>(wrapper.Tree_OnVertexAdded);
            wrapper.Tree.OnEdgeAdded += new EventHandler<EdgesModifiedEventArgs>(wrapper.Tree_OnEdgeAdded);
        }

        void Tree_OnEdgeAdded(object sender, EdgesModifiedEventArgs e)
        {
            if (e.Status == ModificationStatus.Successful)
            {
                this.ArcWrappers.Remove(this.ArcWrappers.Last());
                this.ArcWrappers.Add(new NamedArcWrapper(this, e.Edge as NamedArc));
            }
        }

        void Tree_OnVertexAdded(object sender, VerticesModifiedEventArgs e)
        {
            if(e.Status == ModificationStatus.Successful)
            {
                this.VertexWrappers.Remove(this.VertexWrappers.Last());
                this.VertexWrappers.Add(new MarkingTreeNodeWrapper(this, e.Vertex as MarkingTreeNode));
            }
        }
    }
}
