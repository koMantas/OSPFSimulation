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

        public Node[] AddNewRouter(Node newRouter)
        {
            List<Node> updatedNodes = new List<Node>();
            if (RoutersGraph.AddNode(newRouter))
            {
                UpdateRoutersRoutingTable(updatedNodes,newRouter);
                return updatedNodes.ToArray();
            }
            return null;

        }

        public Node[] RemoveRouter(Node removedRouter)
        {
            List<Node> updatedNodes = new List<Node>();
            if (RoutersGraph.RemoveNode(removedRouter))
            {
                UpdateRoutersRoutingTable(updatedNodes, removedRouter);
                return updatedNodes.ToArray();
            }
            return null;
        }

        public List<PathRouter> FindShortestPath(Node source, Node destination)
        {
            List<PathRouter> path = new List<PathRouter>();
            Dictionary<Node, PreviousPathNode> routingTable = source.RoutingTable;
            PreviousPathNode tempNode = routingTable[destination];
            Node previousNode = destination;
            path.Insert(0, new PathRouter(previousNode, tempNode.CostMetric));
            if (tempNode != null)
            {
                while (tempNode.PathNode != source)
                {
                    previousNode = tempNode.PathNode;
                    tempNode = routingTable[previousNode];
                    path.Insert(0, new PathRouter(previousNode, tempNode.CostMetric));
                }

                return path;
            }
            return null;
        }

        private void CalculateAllRoutersRoutingTable()
        {
            foreach (var router in RoutersGraph.GetGraphNodes())
            {
                DijkstraAlgorithm.FindShortestPath(RoutersGraph, router);
            }
        }

        private void UpdateRoutersRoutingTable(List<Node> updatedNodes, Node addOrRemovedRouter)
        {
            foreach (var item in addOrRemovedRouter.GetNeighbours())
            {
                if (!updatedNodes.Contains(item.NeighborNode))
                {
                    updatedNodes.Add(item.NeighborNode);
                    DijkstraAlgorithm.FindShortestPath(RoutersGraph, item.NeighborNode);
                    UpdateRoutersRoutingTable(updatedNodes, item.NeighborNode);
                }
            }
        }

    }
}
