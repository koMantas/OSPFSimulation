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
            //Test routers
            Node R1 = new Node("R1");
            Node R2 = new Node("R2");
            Node R3 = new Node("R3");
            Node R4 = new Node("R4");
            Node R5 = new Node("R5");
            Node R6 = new Node("R6");
            Node R7 = new Node("R7");
            //Test routers' neighbours
            R1.AddNewNeighbor(R2, 1);
            R1.AddNewNeighbor(R3, 2);
            R1.AddNewNeighbor(R5, 2);
            R2.AddNewNeighbor(R4, 10);
            R4.AddNewNeighbor(R5, 5);
            R4.AddNewNeighbor(R6, 3);
            R5.AddNewNeighbor(R6, 20);
            R5.AddNewNeighbor(R7, 1);
            //Routers added to graph
            Graph graph = new Graph();
            graph.AddNode(R1);
            graph.AddNode(R2);
            graph.AddNode(R3);
            graph.AddNode(R4);
            graph.AddNode(R5);
            graph.AddNode(R6);
            graph.AddNode(R7);

            OSPF ospf = new OSPF(graph);
            bool loopCondition = true;
            string selection;
            while (loopCondition)
            {
                Console.WriteLine("Menu:\n" +
                    "1-See the routers' neighbours\n" +
                    "2-Send packed from A to B\n" +
                    "3-Add new router\n" +
                    "4-Remove router\n" +
                    "0-exit");
                switch (ParseSelection(Console.ReadLine()))
                {
                    case (0):
                        loopCondition = false;
                        break;
                    case (1):
                        foreach (var router in ospf.RoutersGraph.GetGraphNodes())
                        {
                            Console.WriteLine("Node " + router.Info + " ");
                            foreach (var neighbour in router.Neighbors)
                            {
                                Console.WriteLine("Neighbour: "+neighbour.NeighborNode.Info+" Cost metric: "+neighbour.CostMetric);
                            }
                            Console.WriteLine("*********************************************");
                        }
                        break;
                    case (2):
                        Console.Write("Insert source router's name: ");
                        Node source = GetRoutersObjectFromString(Console.ReadLine(),graph);
                        Console.Write("Insert destination router's name: ");
                        Node destination = GetRoutersObjectFromString(Console.ReadLine(),graph);

                        var path = ospf.FindShortestPath(source, destination);
                        if (path != null)
                        {
                            foreach (var pathSteps in path)
                            {
                                Console.WriteLine("Router's name: " + pathSteps.Router.Info + " cost metric: " + pathSteps.CostMetric);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cannot find path from source to destination");
                        }
                        break;
                    case (3):
                        Console.Write("Insert new router's name: ");
                        string name = Console.ReadLine();
                        Node newRouter = new Node(name);
                        Console.WriteLine("Possible neighbours:");
                        foreach(var router in graph.GetGraphNodes())
                        {
                            Console.WriteLine(router.Info);
                        }
                        do
                        {
                            Console.WriteLine("Insert neibour's name(if you want stop adding neigbours, inser 0):");
                            selection = Console.ReadLine();
                            if (!selection.Contains("0"))
                            {
                                Node selectedNode = GetRoutersObjectFromString(selection, graph);
                                Console.Write("Insert cost metric to neigbour: ");
                                int costMetric = ParseSelection(Console.ReadLine());
                                newRouter.AddNewNeighbor(selectedNode,costMetric);
                                Console.WriteLine("New router added");
                            }
                            else
                                break;
                        } while (true);
                        ospf.AddNewRouter(newRouter);
                        break;
                    case (4):
                        Console.Write("Insert removed router's name: ");
                        Node removed = GetRoutersObjectFromString(Console.ReadLine(), graph);
                        ospf.RemoveRouter(removed);
                        break;

                }
                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            }

        }

        private static Node GetRoutersObjectFromString(string routersName, Graph graph)
        {
            Node[] routers = graph.GetGraphNodes();
            Node selection = routers.FirstOrDefault(s => s.Info.Contains(routersName));
            while (true)
            {

                if (selection != null)
                    return selection;
                else
                {
                    Console.Write("Try again to insert router's name:");
                    routersName = Console.ReadLine();
                }
            }
        }

        static Graph LoadTestData()
        {
            //Test routers
            Node R1 = new Node("R1");
            Node R2 = new Node("R2");
            Node R3 = new Node("R3");
            Node R4 = new Node("R4");
            Node R5 = new Node("R5");
            Node R6 = new Node("R6");
            Node R7 = new Node("R7");
            //Test routers' neighbours
            R1.AddNewNeighbor(R2, 1);
            R1.AddNewNeighbor(R3, 2);
            R1.AddNewNeighbor(R5, 2);
            R2.AddNewNeighbor(R4, 10);
            R4.AddNewNeighbor(R5, 5);
            R4.AddNewNeighbor(R6, 3);
            R5.AddNewNeighbor(R6, 20);
            R5.AddNewNeighbor(R7, 1);
            //Routers added to graph
            Graph graph = new Graph();
            graph.AddNode(R1);
            graph.AddNode(R2);
            graph.AddNode(R3);
            graph.AddNode(R4);
            graph.AddNode(R5);
            graph.AddNode(R6);
            graph.AddNode(R6);

            return graph;
        }

       static int ParseSelection(String selection)
        {
            while (true)
            {
                try
                {
                    if (!String.IsNullOrEmpty(selection) || !selection.Equals("\n"))
                        return int.Parse(selection);
                    else
                    {
                        Console.WriteLine("Try again to insert your selecter number");
                        selection = Console.ReadLine();
                    }
                }
                catch
                {
                    Console.WriteLine("Try again to insert your selecter number");
                    selection = Console.ReadLine();
                }
            }
        }
    }
}
