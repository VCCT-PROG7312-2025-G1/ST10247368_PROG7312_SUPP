using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    internal class Graph
    {
        private Dictionary<string, List<Tuple<string, int>>> adjacencyList = new Dictionary<string, List<Tuple<string, int>>>();

        public Graph() { }

        public void insert(string id)
        {
            if(adjacencyList.ContainsKey(id))
            {
                return;
            }

            adjacencyList[id] = new List<Tuple<string, int>>(); 
        }

        public void createEdge(string node1, string node2, int weight = 1, bool undirected = true)
        {
            insert(node1);
            insert(node2);

            adjacencyList[node1].Add(new Tuple<string, int>(node2, weight));

            if(undirected == true)
            {
                adjacencyList[node2].Add(new Tuple<string, int>(node1, weight));
            }
        }

        public List<string> BFS(string start)
        {
            List<string> nodes = new List<string>();

            if (adjacencyList.ContainsKey(start) == false)
            {
                return null;
            }

            HashSet<string> traversed = new HashSet<string>();
            Queue<string> available = new Queue<string>();

            available.Enqueue(start); 
            traversed.Add(start);

            while (available.Count > 0)
            {
                string currentNode = available.Dequeue();

                nodes.Add(currentNode);

                foreach (var elem in adjacencyList[currentNode])
                {
                    if (traversed.Contains(elem.Item1) == false)
                    {
                        traversed.Add(elem.Item1);
                        available.Enqueue(elem.Item1);
                    }
                }
            }

            return nodes;
        }


    }
}
