namespace CHR_pathfindingAPI
{
    public class Node
    {
        private int _id;
        private List<Node> _neighbors;
        //graph node structure
        public Node(int id) 
            {
            _neighbors = new List<Node>();
            _id = id;
            }

        //unique id of node
        public int Id { get { return _id; } }

        //gets list on neighbors
        public List<Node> GetNeighborsReferences()
        {
            return _neighbors;
        }

        //checks if node is neighbor
        public bool CheckForNeighbor(Node node)
        {
            return _neighbors.Contains(node);
        }
        //adds node as new neighbor, adds current caller as neighbor for node
        public void AddNeighbor(Node node)
        {
            if (node != this && !CheckForNeighbor(node))
            {
                _neighbors.Add(node);
                node.AddNeighbor(this);
            }
            
        }
        //removes node from neighbors, removes current caller from neighbors of node
        public void DeleteNeighbor(Node node)
        {
            if (node != this && CheckForNeighbor(node))
            {
                _neighbors.Remove(node);
                node.DeleteNeighbor(this);
            }

        }
    }
    public class Graph
    {
        Dictionary<string, Node> _graph;
        string[] _names;
        public Graph() 
            {
            InitialiseGraph();
        }
        //finds the shortest path using dijkstra algorithm
        public List<string> GetPath(string start, string end)
        {
            if (!_graph.ContainsKey(start))
            {
                throw new ArgumentException(start);
            }
            if (!_graph.ContainsKey(end))
            {
                throw new ArgumentException(end);
            }
            if (start == end)
            {
                return new List<string>() { start };
            }
            Queue<Node> queue = new Queue<Node>();
            int[] dists = new int[_graph.Count];
            Node[] prev = new Node[_graph.Count];
            for (int i = 0; i < _graph.Count; i++)
            {
                dists[i] = int.MaxValue;
            }
            dists[_graph[start].Id] = 0;
            queue.Enqueue(_graph[start]);
            while (queue.Count != 0)
            {
                Node n = queue.Dequeue();
                foreach (Node neighbor in n.GetNeighborsReferences())
                {
                    if (dists[n.Id] + 1 < dists[neighbor.Id])
                    {
                        dists[neighbor.Id] = dists[n.Id] + 1;
                        prev[neighbor.Id] = n;
                        if (neighbor == _graph[end])
                        {
                            List<string> ls2 = new List<string>();
                            Node pv2 = prev[_graph[end].Id];
                            ls2.Add(_names[_graph[end].Id]);
                            while (pv2 != null)
                            {
                                ls2.Add(_names[pv2.Id]);
                                pv2 = prev[pv2.Id];
                            }
                            ls2.Reverse();
                            return ls2;
                        }
                        else
                        {
                            queue.Enqueue(neighbor);
                        }
                        
                    }
                }
            }
            if (dists[_graph[end].Id] == int.MaxValue)
            {
                return new List<string>() { $"THERE IS NO ROUTE FROM {_names[_graph[start].Id]} TO {_names[_graph[end].Id]}" };
            }
            List<string> ls = new List<string>();
            Node pv = prev[_graph[end].Id];
            ls.Add(_names[_graph[end].Id]);
            while (pv != null)
            {
                ls.Add(_names[pv.Id]);
                pv = prev[pv.Id];
            }
            ls.Reverse();
            return ls;
        }
        private void InitialiseGraph()
        {
            _graph = new Dictionary<string, Node>();
            _graph.Add("CAN", new Node(0));
            _graph.Add("USA", new Node(1));
            _graph.Add("MEX", new Node(2));
            _graph.Add("BLZ", new Node(3));
            _graph.Add("GTM", new Node(4));
            _graph.Add("SLV", new Node(5));
            _graph.Add("HND", new Node(6));
            _graph.Add("NIC", new Node(7));
            _graph.Add("CRI", new Node(8));
            _graph.Add("PAN", new Node(9));
            _graph["CAN"].AddNeighbor(_graph["USA"]);
            _graph["USA"].AddNeighbor(_graph["MEX"]);
            _graph["MEX"].AddNeighbor(_graph["GTM"]);
            _graph["MEX"].AddNeighbor(_graph["BLZ"]);
            _graph["GTM"].AddNeighbor(_graph["BLZ"]);
            _graph["GTM"].AddNeighbor(_graph["SLV"]);
            _graph["GTM"].AddNeighbor(_graph["HND"]);
            _graph["SLV"].AddNeighbor(_graph["HND"]);
            _graph["NIC"].AddNeighbor(_graph["HND"]);
            _graph["NIC"].AddNeighbor(_graph["CRI"]);
            _graph["PAN"].AddNeighbor(_graph["CRI"]);
            _names = new string[] { "CAN", "USA", "MEX", "BLZ", "GTM", "SLV", "HND", "NIC", "CRI", "PAN" };
        }
    }
}
