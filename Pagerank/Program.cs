// See https://aka.ms/new-console-template for more information
Graph graph = new Graph();

graph.addNode("1");
graph.addNode("2");
graph.addNode("3");
graph.addNode("4");

graph.addEdge("1", "2");
graph.addEdge("2", "3");
graph.addEdge("2", "4");
graph.addEdge("3", "2");
graph.addEdge("4", "3");

Boolean normalizeScores = false;

Dictionary<string, double> tokens = new PageRankMgr(graph).getPageRanks(normalizeScores);

foreach(var token in tokens)
{
    Console.WriteLine(token);
}
