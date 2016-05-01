using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterProtocol.GraphStructure;

namespace RouterProtocol
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test
            Node A = new Node("A");
            Node B = new Node("B");
            Node C = new Node("C");
            Node D = new Node("D");
            Node E = new Node("E");
            A.AddNewNeighbor(B, 3);
            A.AddNewNeighbor(C, 6);
            B.AddNewNeighbor(C, 1);
            B.AddNewNeighbor(D, 6);
            C.AddNewNeighbor(D, 1);
            A.AddNewNeighbor(E, 7);
            C.AddNewNeighbor(E, 1);
            //List<Node> nodes = new List<Node>();
            //nodes.Add(A);
            //nodes.Add(B);
            //nodes.Add(C);
            //nodes.Add(D);
            Graph graph = new Graph();
            graph.AddNode(A);
            graph.AddNode(B);
            graph.AddNode(C);
            graph.AddNode(D);
            graph.AddNode(E);

            //var paths = DijkstraAlgorithm.FindShortestPath(graph, A);
            //foreach(var key in paths.Keys)
            //{
            //    Console.WriteLine("Node: " + key.Info + " distance: " + paths[key]);
            //}

            var ospf = new OSPF(graph);
            foreach (var pathSteps in ospf.FindShortestPath(A, A))
            {
                Console.WriteLine("Router's name: "+ pathSteps.Router.Info + " cost metric: "+pathSteps.CostMetric);
            }

            Console.WriteLine("****************************************");

            foreach (var pathSteps in ospf.FindShortestPath(A, D))
            {
                Console.WriteLine("Router's name: " + pathSteps.Router.Info + " cost metric: " + pathSteps.CostMetric);
            }
        }
    }
}
