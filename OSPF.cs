using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterProtocol.GraphStructure;

namespace RouterProtocol
{
    public class OSPF
    {
        private Graph RoutersGraph { get; set; }

        public OSPF(Graph routersGraph)
        {
            RoutersGraph = routersGraph;
            CalculateAllRoutersRoutingTable();
        }

        public bool AddNewRouter(Node newRouter)
        {
            return RoutersGraph.AddNode(newRouter);
        }

        public bool RemoveRouter(Node removedRouter)
        {
            return RoutersGraph.RemoveNode(removedRouter);
        }

        public List<PathRouter> FindShortestPath(Node source, Node destination)
        {
            List<PathRouter> path = new List<PathRouter>();
            Dictionary<Node, PreviousPathNode> routingTable = source.RoutingTable;
            PreviousPathNode tempNode = routingTable[destination];
            path.Insert(0, new PathRouter(tempNode.PathNode, tempNode.CostMetric));
            if (tempNode != null)
            {
                while (tempNode.PathNode != source)
                {
                    tempNode = routingTable[tempNode.PathNode];
                    path.Insert(0, new PathRouter(tempNode.PathNode, tempNode.CostMetric));
                }

                return path;
            }
            return null;
        }

        private void CalculateAllRoutersRoutingTable()
        {
            foreach(var router in RoutersGraph.GetGraphNodes())
            {
                DijkstraAlgorithm.FindShortestPath(RoutersGraph, router);
            }
        }


    }
}
