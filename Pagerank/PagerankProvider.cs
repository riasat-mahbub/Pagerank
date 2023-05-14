using System;
using System.Collections.Generic;

public class PagerankProvider
{
	public DirectedGraph DirectedGraph = new DirectedGraph();
	public Dictionary<string, double> tokens{ get; set; }

	private Dictionary<string , double> oldScores = new Dictionary<string , double>();
	private Dictionary<string, double> newScores = new Dictionary<string , double>();


	private double significantThreshhold = StaticData.SIGNIFICANT_THRESHHOLD;
	private double dampingFactor = StaticData.DAMPING_FACTOR;
	private double initialNodeScore = StaticData.INITIAL_VERTEX_SCORE;
	private int maxIterations = StaticData.MAX_ITERATIONS;

    public PagerankProvider(DirectedGraph graph, Dictionary<string, double> tokens)
	{
		DirectedGraph = graph;
		this.tokens = tokens;
	}

	Boolean checkSignificantDiff(double oldV, double newV)
	{
		double diff = newV > oldV ? newV - oldV : oldV - newV;
		return diff > significantThreshhold;
	}

	public Dictionary<string, double> calculatePageRank(Boolean normalize)
	{
		int n = DirectedGraph.getAllNodes().Count;
		foreach(var node in DirectedGraph.getAllNodes())
		{
			oldScores[node] = initialNodeScore;
			newScores[node] = initialNodeScore;
		}

		Boolean enoughIterations = false;
		int itercount = 0;

		while (!enoughIterations)
		{
			int insignificant = 0;
			foreach(var nodeName in DirectedGraph.getAllNodes())
			{
                List < Tuple<string, string> > incomingEdges = DirectedGraph.getIncomingEdgesof(nodeName);
				double tokenRank = (1-dampingFactor);
				double comingScore = 0;
				foreach(var edge in incomingEdges){
					string source1 = edge.Item1;
					int outDegree = DirectedGraph.getOutDegreeof(source1);

					double score = oldScores[source1];

					if(outDegree == 1)
					{
						comingScore += score;
					}else if(outDegree > 1)
					{
						comingScore += (score / outDegree);
					}

				}

				comingScore *= dampingFactor;
				tokenRank += comingScore;
				Boolean isSignificant = checkSignificantDiff(oldScores[nodeName], tokenRank);

				if (isSignificant)
				{
					newScores[nodeName] = tokenRank;
				}
				else
				{
					insignificant++;
				}
			}

			foreach(var item in newScores)
			{
				oldScores[item.Key] = item.Value;
			}

			itercount++;

			if (itercount == maxIterations || insignificant == DirectedGraph.getAllNodes().Count())
			{
				enoughIterations = true;
			}
		}
		if (normalize)
		{
			recordNormalizedScores();
		}
		else
		{
			recordOriginalScores();
		}
		return this.tokens;

	}

	protected void recordOriginalScores()
	{
		foreach(var key in newScores)
		{
			tokens[key.Key] = key.Value;
		}
	}

	protected void recordNormalizedScores()
	{
		double maxRank = 0;
		foreach(var item in newScores)
		{
			double score = oldScores[item.Key];
			if(score> maxRank)
			{
				maxRank = score;
			}
		}

        foreach (var item in newScores)
        {
            double score = oldScores[item.Key];
			score = score / maxRank;
			tokens[item.Key] = score;
        }
    }
}
