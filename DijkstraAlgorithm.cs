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
        public static Dictionary<Node,int> FindShortestPath(Graph graph, Node source)
        {
            List<Node> nodes = new List<Node>();
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
            foreach (var node in graph.GetGraphNodes())
            {
                distance.Add(node, int.MaxValue);

                nodes.Add(node);
            };
            distance[source] = 0;

            while (nodes.Count != 0)
            {
                Node temp = GetNearestNode(nodes.ToArray(),distance);
                nodes.Remove(temp);

                foreach(var neighbour in temp.GetNeighbours())
                {
                    int tempDistance = distance[temp] + neighbour.CostMetric;
                    if (tempDistance < distance[neighbour.NeighborNode])
                    {
                        //shortest path is found
                        distance[neighbour.NeighborNode] = tempDistance;
                        prev[neighbour.NeighborNode] = temp; 
                    }
                }
            }

            foreach(var item in prev.Keys)
            {
                Console.WriteLine("To: "+ item.Info + " previous:"+prev[item].Info);
            }

            return distance;



            //foreach(var node in distance.Keys.ToArray())
            //{
            //    Node nearestNode = GetNearestNode(distance, isFinalized);

            //    isFinalized[nearestNode] = true;

            //    foreach (var neighbour in nearestNode.GetNeighbours())
            //    {
            //        var neighbourNode = neighbour.NeighborNode;
            //        var neighbourDistance = neighbour.CostMetric;

                    
            //    }

            //}
            



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

        private static Node GetNearestNode(Dictionary<Node, int> distance, Dictionary<Node, bool> isFinalized)
        {
            Node minimumDistanceNode = null;
            int minDistance = int.MaxValue;
            foreach(var node in distance.Keys.ToArray())
            {
                if(isFinalized[node] == false && distance[node]<= minDistance)
                {
                    minDistance = distance[node];
                    minimumDistanceNode = node;
                }
            }
            return minimumDistanceNode;
        }
    }
}
