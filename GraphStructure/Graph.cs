using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterProtocol.GraphStructure
{
    public class Graph
    {
        private List<Node> _nodes = new List<Node>();

        public Graph(){}

        public Graph(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node);
            }
        }

        public bool AddNode(Node newNode)
        {
            if (!_nodes.Contains(newNode))
            {
                _nodes.Add(newNode);
                return true;
            }
            return false;
        }

        public bool RemoveNode(Node removedNode)
        {
            Node temp = _nodes.FirstOrDefault(s => s.ID == removedNode.ID);
            if(temp != null)
            {
                foreach(var neighbour in temp.GetNeighbours())
                    neighbour.NeighborNode.RemoveNeighbor(temp);
                _nodes.Remove(temp);
                return true;
            }
            return false;
        }

        public Node[] GetGraphNodes()
        {
            return _nodes.ToArray();
        }
    }
}
