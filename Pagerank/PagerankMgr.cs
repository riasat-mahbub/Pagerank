using System;

public class PageRankMgr
{
	public DirectedGraph graph { get; set; }
	public DirectedGraphWeighted wgraph { get; set; }

	private Boolean isGraphWeighted;
	public PageRankMgr(DirectedGraph graph)
	{
		this.graph = graph;
		this.isGraphWeighted = false;
	}
	public PageRankMgr(DirectedGraphWeighted graph)
	{
		this.wgraph = graph;
		this.isGraphWeighted = true;
	}

    public Dictionary<string, double> getPageRanks(Boolean normalize)
	{
		if (isGraphWeighted)
		{
			return getPageRanksWeighted(normalize);
		}
		else
		{
			return getPageRanksUnweighted(normalize);
		}

	}



    public Dictionary<string, double> getPageRanksUnweighted(Boolean normalize)
	{
		List<string> nodes = graph.getAllNodes();
		Dictionary<string, double> tokens = new Dictionary<string, double>();

		PagerankProvider pgRank = new PagerankProvider(graph, tokens);
		return pgRank.calculatePageRank(normalize);
	}

	public Dictionary<string, double> getPageRanksWeighted( Boolean normalize)
	{
        List<string> nodes = wgraph.getAllNodes();
        Dictionary<string, double> tokens = new Dictionary<string, double>();

        PagerankProvider pgRank = new PagerankProvider(wgraph, tokens);
        return pgRank.calculatePageRankWeighted(normalize);
    }
}
