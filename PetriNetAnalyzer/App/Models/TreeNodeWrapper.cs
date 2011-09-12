using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GraphEditor.App.Models;
using System.Drawing;

using MagicLibrary.MathUtils.Graphs.Trees;
using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils;
using MagicLibrary.Graphic;

namespace PetriNetAnalyzer.App.Models
{
    public class TreeNodeWrapper : WFVertexWrapper
    {
        public Font Font { get; set; }

        public TreeNodeWrapper ParentWrapper { get { return this.TreeWrapper.VertexWrappers.Find(v => v.EqualsVetices(this.Node.Parent as IVertex)) as TreeNodeWrapper; } }

        public TreeGraphNode Node { get { return this.Vertex as TreeGraphNode; } set { this.Vertex = value; } }

        public TreeGraphWrapper TreeWrapper { get { return this.graphWrapper as TreeGraphWrapper; } }

        public virtual string Text { get { return this.Name; } }

        public List<TreeNodeWrapper> Children
        {
            get
            {
                List<TreeNodeWrapper> children = new List<TreeNodeWrapper>();
                this.Node.Children.ForEach(child => children.Add(this.TreeWrapper[(child as TreeGraphNode).Value.ToString()]));
                return children;
            }
        }

        public TreeNodeWrapper(TreeGraphWrapper wrapper, TreeGraphNode node)
            : base(wrapper, node)
        {
           this.Font = new Font("Arial", 8);
        }

        public double GetWidthWithChild(Graphics g, double dX)
        {
            double width = 0;
            this.Children.ForEach(child => width += child.GetWidthWithChild(g, dX) + 2 * dX);
            return Math.Max(width, this.GetWidth(g) + 2 * dX);
        }

        public double GetWidth(Graphics g)
        {
            return this.CalculateSize(g).Width;
        }

        public virtual SizeF CalculateSize(Graphics g)
        {
            return g.MeasureString(this.Text, this.Font);
        }

        public override void Draw(Graphics g, Pen p)
        {
            this.SizeF = this.CalculateSize(g);
            Rectangle r = Rectangle;

            g.DrawRectangle(p, r);

            g.DrawString(
                this.Text,
                Font,
                Brushes.Blue,
                r.Location
            );
        }

        public override void Draw(Graphics g, Pen p, Matrix m)
        {
            this.SizeF = this.CalculateSize(g);
            Rectangle r = Rectangle;

            g.DrawLines(p, m * r);
            
            g.DrawString(
                this.Text,
                Font,
                Brushes.Blue,
                m * r.Location
            );
        }
    }
}
