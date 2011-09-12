using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using GraphEditor.App.Models;

using MagicLibrary.MathUtils.Graphs.Trees;
using MagicLibrary.MathUtils.Graphs;

namespace PetriNetAnalyzer.App.Models
{
    public class TreeGraphWrapper : WFGraphWrapper
    {
        private double dY = 45;
        private double dX = 5;
        public PointF RootPosition { get; set; }

        public TreeGraphWrapper(TreeGraph tree = null)
            : base()
        {
            if (tree == null)
                this.Graph = new TreeGraph();
            else
            {
                this.Graph = tree;

                this.Tree.GetVertices().ForEach(v => this.VertexWrappers.Add(new TreeNodeWrapper(this, v as TreeGraphNode)));
                this.Tree.GetEdges().ForEach(e => this.ArcWrappers.Add(new WFArcWrapper(this, e as Arc)));
            }
            this.RootPosition = new PointF();

            TreeGraphWrapper.SetDefaultEventHandlers(this);
        }

        public TreeNodeWrapper RootWrapper { get { return this.VertexWrappers.Find(v => v.EqualsVetices(this.Tree.Root as IVertex)) as TreeNodeWrapper; } }

        public new TreeNodeWrapper this[string name]
        {
            get
            {
                return this.VertexWrappers.Find(v => v.Name == name) as TreeNodeWrapper;
            }
        }

        public TreeGraph Tree { get { return this.Graph as TreeGraph; } }

        public static void SetDefaultEventHandlers(TreeGraphWrapper wrapper)
        {
            WFGraphWrapper.SetDefaultEventHandlers(wrapper);

            wrapper.Tree.OnVertexAdded += new EventHandler<MagicLibrary.MathUtils.Graphs.VerticesModifiedEventArgs>(wrapper.Tree_OnVertexAdded);
        }

        void Tree_OnVertexAdded(object sender, VerticesModifiedEventArgs e)
        {
            if (e.Status == ModificationStatus.Successful)
            {
                var last = this.VertexWrappers.Last();
                this.VertexWrappers.Remove(last);
                this.VertexWrappers.Add(new TreeNodeWrapper(this, e.Vertex as TreeGraphNode));
            }
        }

        public void ResetAllNodesPositions(Graphics g)
        {
            //this.RootWrapper.Center = this.RootPosition;
            if (this.RootWrapper == null)
                return;
            ResetNodePositionWithChilds(this.RootWrapper, this.RootPosition, g);
        }

        public void ResetNodePositionWithChilds(TreeNodeWrapper nodeWrapper, PointF position, Graphics g)
        {
            nodeWrapper.SizeF = nodeWrapper.CalculateSize(g);
            nodeWrapper.Center = position;
            double width = nodeWrapper.GetWidthWithChild(g, this.dX);
            double x = position.X - width / 2;
            nodeWrapper.Children.ForEach(delegate(TreeNodeWrapper child)
            {
                double w = child.GetWidthWithChild(g, this.dX);
                // set coords at the center in it's rect
                this.ResetNodePositionWithChilds(
                    child,
                    new PointF((float)(x + w / 2),
                                (float)(position.Y + this.dY)),
                    g
                );
                x += w;
            });
        }

        public void Draw(Graphics g, Pen p, MagicLibrary.MathUtils.Matrix m, bool resetPositions)
        {
            if(resetPositions)
                this.ResetAllNodesPositions(g);
            base.Draw(g, p, m);
        }
    }
}
