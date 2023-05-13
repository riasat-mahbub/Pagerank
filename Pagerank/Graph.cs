using System;
using System.Collections;

public class Graph
{
	public List< Tuple<string, List<string>>> graph = new List<Tuple<string, List<string>>>(); 
	public Graph()
	{
		graph = new List<Tuple<string, List<string>>>();
    }

	public void addNode(string nodeName)
	{
		Tuple<string, List<string>> newNode = new Tuple<string, List<string>>(nodeName, new List<string>());
        graph.Add(newNode);
	}

	public void addEdge(string source, string destination)
	{
		List<string> sourceNode =  graph.Find(pair => pair.Item1.Equals(source)).Item2;
		sourceNode.Add(destination);
	}

	public List<string> getAllNodes()
	{
		List<string> allNodes = new List<string>();
		foreach(var node in graph)
		{
			allNodes.Add(node.Item1);
		}
		return	allNodes;
	}

	public List<Tuple<string, string>> getIncomingEdgesof(string nodeName)
	{
		//get incoming edges as a pair of strings, the 1st item is incoming, 2nd is nodeName
		List<Tuple<string, string>> incomingEdges = new List<Tuple<string, string>>();
		foreach(var node in graph)
		{
			if (node.Item2.Contains(nodeName))
			{
				incomingEdges.Add(new Tuple<string, string>(node.Item1, nodeName));
			}
		}
		return incomingEdges;
    }

	public int getOutDegreeof(string nodeName)
	{
		List<string> connections = graph.Find(pair => pair.Item1.Equals(nodeName)).Item2;
		return connections.Count;
	}

	public void showGraph()
	{
		foreach (var node in graph)
		{
			Console.Write(node.Item1 + "[  ");
			foreach(var edge in node.Item2)
			{
				Console.Write(edge+" ");
			}
			Console.WriteLine(" ]");
		}
	}
}
