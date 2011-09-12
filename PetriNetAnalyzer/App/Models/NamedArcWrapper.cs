using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GraphEditor.App.Models;
using MagicLibrary.MathUtils.Graphs;

namespace PetriNetAnalyzer.App.Models
{
    class NamedArcWrapper : WFArcWrapper
    {
        public NamedArc Arc { get { return this.Edge as NamedArc; } }

        public NamedArcWrapper(IGraphWrapper graphWrapper, NamedArc arc)
            : base(graphWrapper, arc)
        {

        }

        public override string Text
        {
            get
            {
                return Arc.Name;
            }
        }
    }
}
