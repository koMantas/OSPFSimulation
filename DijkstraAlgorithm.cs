using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterProtocol.GraphStructure;

namespace RouterProtocol
{
    public static class DijkstraAlgorithm
    {

        //source is the starting point from which algorithm calculates all paths to other nodes
        public static Dictionary<Node, int> FindShortestPath(Graph graph, Node source)
        {
            List<Node> nodes = new List<Node>();
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            source.RoutingTable = new Dictionary<Node, PreviousPathNode>();
            source.RoutingTable.Add(source, new PreviousPathNode(source, 0));
            foreach (var node in graph.GetGraphNodes())
            {
                distance.Add(node, int.MaxValue);

                nodes.Add(node);
            };
            distance[source] = 0;

            while (nodes.Count != 0)
            {
                Node temp = GetNearestNode(nodes.ToArray(), distance);
                nodes.Remove(temp);

                foreach (var neighbour in temp.GetNeighbours())
                {
                    int tempDistance = distance[temp] + neighbour.CostMetric;
                    if (tempDistance < distance[neighbour.NeighborNode])
                    {
                        //shortest path is found
                        distance[neighbour.NeighborNode] = tempDistance;
                        //Previous node to destination node
                        source.RoutingTable[neighbour.NeighborNode] = new PreviousPathNode(temp, neighbour.CostMetric);
                    }
                }
            }

            return distance;
        }

        private static Node GetNearestNode(Node[] nodes, Dictionary<Node, int> distance)
        {
            Node minimumDistanceNode = null;
            int minDistance = int.MaxValue;
            foreach (var node in nodes)
            {
                if (distance[node] <= minDistance)
                {
                    minDistance = distance[node];
                    minimumDistanceNode = node;
                }
            }
            return minimumDistanceNode;
        }

    }
}
