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
        public int ID { get; private set; }
        public string Info { get; private set; }
        public List<Neighbor> Neighbors { get; private set; }

        public Node(string info)
        {
            Info = info;
            ID = numOfNodes++;
            Neighbors = new List<Neighbor>();
            this.AddNewNeighbor(this, 0);
        }


        public bool AddNewNeighbor(Node newNeighbor, int costMetric)
        {
            if (Neighbors.FirstOrDefault(s => s.NeighborNode.ID == newNeighbor.ID) != null)
            {
                newNeighbor.AddNewNeighbor(this, costMetric);
                Neighbors.Add(new Neighbor(newNeighbor, costMetric));
                return true;
            }
            return false;
        }

        public bool RemoveNeighbor(Node removedNeighbor)
        {
            Neighbor temp = Neighbors.FirstOrDefault(s => s.NeighborNode.ID == removedNeighbor.ID);
            if (temp != null)
            {
                removedNeighbor.RemoveNeighbor(this);
                Neighbors.Remove(temp);
                return true;
            }
            return false;
        }

        public Neighbor[] GetNeighbours()
        {
            return Neighbors.ToArray();
        }

        public int[] GetNeighboursIDs()
        {
            return Neighbors.Select(s => s.NeighborNode.ID).ToArray();
        }
    }
}
