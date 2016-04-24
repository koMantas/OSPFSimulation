using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterProtocol.Graph
{
    public class Graph
    {
        private List<Node> _nodes = new List<Node>();

        public Graph(){}

        public bool AddNode(Node newNode)
        {
            if (_nodes.Contains(newNode))
            {
                _nodes.Add(newNode);
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
