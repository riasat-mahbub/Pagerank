using System;
using System.Collections.Generic;

public class PageRank
{
	public Graph Graph = new Graph();
	public Dictionary<string, double> tokens{ get; set; }

	private Dictionary<string , double> oldScores = new Dictionary<string , double>();
	private Dictionary<string, double> newScores = new Dictionary<string , double>();


	const double SIGNIFICANT_THRESHHOLD = 0.001;
	const double DAMPING_FACTOR = 0.85;
	const double INITIAL_VERTEX_SCORE = 0.25;
	const int MAX_ITERATIONS = 100;

    public PageRank(Graph graph, Dictionary<string, double> tokens)
	{
		Graph = graph;
		this.tokens = tokens;
	}

	Boolean checkSignificantDiff(double oldV, double newV)
	{
		double diff = newV > oldV ? newV - oldV : oldV - newV;
		return diff > SIGNIFICANT_THRESHHOLD;
	}

	public Dictionary<string, double> calculatePageRank(Boolean normalize)
	{
		int n = Graph.getAllNodes().Count;
		foreach(var node in Graph.getAllNodes())
		{
			oldScores[node] = INITIAL_VERTEX_SCORE;
			newScores[node] = INITIAL_VERTEX_SCORE;
		}

		Boolean enoughIterations = false;
		int itercount = 0;

		while (!enoughIterations)
		{
			int insignificant = 0;
			foreach(var nodeName in Graph.getAllNodes())
			{
                List < Tuple<string, string> > incomingEdges = Graph.getIncomingEdgesof(nodeName);
				double tokenRank = (1-DAMPING_FACTOR);
				double comingScore = 0;
				foreach(var edge in incomingEdges){
					string source1 = edge.Item1;
					int outDegree = Graph.getOutDegreeof(source1);

					double score = oldScores[source1];

					if(outDegree == 1)
					{
						comingScore += score;
					}else if(outDegree > 1)
					{
						comingScore += (score / outDegree);
					}

				}

				comingScore *= DAMPING_FACTOR;
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

			if (itercount == MAX_ITERATIONS || insignificant == Graph.getAllNodes().Count())
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
