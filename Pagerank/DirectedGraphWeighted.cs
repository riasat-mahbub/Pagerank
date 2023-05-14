using System;
using System.Collections;
using System.Collections.Generic;

public class DirectedGraphWeighted
{
    public List<Tuple<string, List< Tuple<string, int> > > > graph = new List<Tuple<string, List<Tuple<string, int>>>>();
    public DirectedGraphWeighted()
    {
        graph = new List<Tuple<string, List<Tuple<string, int>>>>();
    }

    public void addNode(string nodeName)
    {
        Tuple<string, List<Tuple<string, int>>> newNode = new Tuple<string, List<Tuple<string, int>>>(nodeName, new List<Tuple<string, int>>());
        graph.Add(newNode);
    }

    public void addEdge(string source, string destination, int weight=0)
    {
        List<Tuple<string,int>> sourceNode = graph.Find(pair => pair.Item1.Equals(source)).Item2;
        sourceNode.Add(new Tuple<string, int>(destination, weight));
    }

    public List<string> getAllNodes()
    {
        List<string> allNodes = new List<string>();
        foreach (var node in graph)
        {
            allNodes.Add(node.Item1);
        }
        return allNodes;
    }

    public List<Tuple<string, string>> getIncomingEdgesof(string nodeName)
    {
        //get incoming edges as a pair of strings, the 1st item is incoming, 2nd is current node
        List<Tuple<string, string>> incomingEdges = new List<Tuple<string, string>>();
        foreach (var node in graph)
        {
            List< Tuple<string, int>> outgoingEdges = node.Item2;
            if (outgoingEdges.Any(edge => edge.Item1 == nodeName))
            {
                incomingEdges.Add(new Tuple<string, string>(node.Item1, nodeName));
            }
        }
        return incomingEdges;
    }

    public int getOutDegreeof(string nodeName)
    {
        List<Tuple<string, int> > connections = graph.Find(pair => pair.Item1.Equals(nodeName)).Item2;
        return connections.Count;
    }

    public void showGraph()
    {
        foreach (var node in graph)
        {
            Console.Write(node.Item1 + "[  ");
            foreach (var edge in node.Item2)
            {
                Console.Write(edge + " ");
            }
            Console.WriteLine(" ]");
        }
    }
}
