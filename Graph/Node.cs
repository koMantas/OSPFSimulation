using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterProtocol.Graph
{
    public class Node
    {
        private static int numOfNodes = 0;
        public Dictionary<int, Node> Vertexes { get; private set; }
        public int ID{ get; private set; }
        public string Info { get; private set; }

        public Node(string info)
        {
            Info = info;
            ID = numOfNodes++;
            Vertexes = new Dictionary<int, Node>();
        }


        public bool AddNewVertex(Node newVertex)
        {
            if (Vertexes.ContainsKey(newVertex.ID))
            {
                Vertexes.Add(newVertex.ID, newVertex);
                return true;
            }
            return false;
        }

        public bool RemoveVertex(int removedVertexId)
        {
            if (Vertexes.ContainsKey(removedVertexId))
            {
                Vertexes.Remove(removedVertexId);
                return true;
            }
            return false;
        }
    }
}
