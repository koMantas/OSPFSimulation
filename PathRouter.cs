using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterProtocol.GraphStructure;

namespace RouterProtocol
{
    public class PathRouter
    {
        public Node Router { get; private set; }
        public int CostMetric { get; private set; }

        public PathRouter(Node router, int costMetric)
        {
            Router = router;
            CostMetric = costMetric;
        }
    }
}
