using System;
using System.Collections;

public class DirectedGraph
{
	private DirectedGraphWeighted directedGraphWeighted;
    public List<Tuple<string, List<Tuple<string, int>>>> graph = new List<Tuple<string, List<Tuple<string, int>>>>();
    public DirectedGraph()
	{
		directedGraphWeighted = new DirectedGraphWeighted();
		graph = directedGraphWeighted.graph;
    }

	public void addNode(string nodeName)
	{
		directedGraphWeighted.addNode(nodeName);
	}

	public void addEdge(string source, string destination)
	{
		directedGraphWeighted.addEdge(source, destination);
	}

	public List<string> getAllNodes()
	{
		return directedGraphWeighted.getAllNodes();
	}

	public List<Tuple<string, string>> getIncomingEdgesof(string nodeName)
	{
		return directedGraphWeighted.getIncomingEdgesof(nodeName);
    }

	public int getOutDegreeof(string nodeName)
	{
		return directedGraphWeighted.getOutDegreeof(nodeName);
	}

	public void showGraph()
	{
		directedGraphWeighted.showGraph();
	}
}
