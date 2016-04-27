using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterProtocol.Graph
{
    public class Neighbor
    {
        public Node NeighborNode { get; private set; }
        public int CostMetric { get; private set; }

        public Neighbor(Node neighbor, int costMetric)
        {
            NeighborNode = neighbor;
            CostMetric = costMetric;
        }
    }
}
