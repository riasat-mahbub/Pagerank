using System;

public class PageRankMgr
{
	public DirectedGraph graph { get; set; }

	public PageRankMgr(DirectedGraph graph)
	{
		this.graph = graph;
	}

	public Dictionary<string, double> getPageRanks(Boolean normalize)
	{
		List<string> nodes = graph.getAllNodes();
		Dictionary<string, double> tokens = new Dictionary<string, double>();

		PagerankProvider pgRank = new PagerankProvider(graph, tokens);
		return pgRank.calculatePageRank(normalize);
	}
}
