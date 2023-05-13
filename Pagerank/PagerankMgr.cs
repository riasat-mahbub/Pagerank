using System;

public class PageRankMgr
{
	public Graph graph { get; set; }

	public PageRankMgr(Graph graph)
	{
		this.graph = graph;
	}

	public Dictionary<string, double> getPageRanks(Boolean normalize)
	{
		List<string> nodes = graph.getAllNodes();
		Dictionary<string, double> tokens = new Dictionary<string, double>();

		PageRank pgRank = new PageRank(graph, tokens);
		return pgRank.calculatePageRank(normalize);
	}
}
