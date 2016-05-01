using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterProtocol.GraphStructure
{
    public class PreviousPathNode
    {
        public Node PathNode { get; private set; }
        public int CostMetric { get; private set; }

        public PreviousPathNode(Node pathNode, int costMetric)
        {
            PathNode = pathNode;
            CostMetric = costMetric;
        }
    }
}
