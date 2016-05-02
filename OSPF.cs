using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterProtocol.GraphStructure;
using System.Threading;

namespace RouterProtocol
{
    public class OSPF
    {
        public Graph RoutersGraph { get; set; }

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
                UpdateRoutersRoutingTable(updatedNodes, newRouter);
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
            if (routingTable.Keys.FirstOrDefault(s => s == destination) != null)
            {
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
            }
            return null;
        }

        public void SendPacket(string packet, Node source, Node destination)
        {
            List<PathRouter> path = FindShortestPath(source, destination);
            Node currentRouter = source;
            int timeout = 0;
            while (currentRouter != destination)
            {
                if (path != null)
                {
                    Thread.Sleep(15000);
                    if (currentRouter.GetNeighbours().FirstOrDefault(s => s.NeighborNode == path[0].Router) != null)
                    {
                        Console.WriteLine("Packet \"" + packet + "\" is in router " + path[0].Router.Info + " and cost metric to router is " + path[0].CostMetric);
                        currentRouter = path[0].Router;
                        path = FindShortestPath(currentRouter, destination);
                        timeout = 0;
                    }
                    else
                    {
                        timeout++;
                        if (timeout == 3)
                        {
                            Console.WriteLine("Packet \"" + packet + "\" Cannot reach router " + path[0].Router.Info);
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Cannot send packet to " + destination);
                    break;
                }
            }
            Console.WriteLine("Packet \"" + packet + "\" reached destination");
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
                if (!updatedNodes.Contains(item.NeighborNode) && item.NeighborNode != addOrRemovedRouter)
                {
                    updatedNodes.Add(item.NeighborNode);
                    DijkstraAlgorithm.FindShortestPath(RoutersGraph, item.NeighborNode);
                    UpdateRoutersRoutingTable(updatedNodes, item.NeighborNode);
                }
            }
        }

    }
}
