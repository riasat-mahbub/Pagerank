// See https://aka.ms/new-console-template for more information
DirectedGraph graph = new DirectedGraph();

graph.addNode("1");
graph.addNode("2");
graph.addNode("3");
graph.addNode("4");

graph.addEdge("1", "2");
graph.addEdge("2", "3");
graph.addEdge("2", "4");
graph.addEdge("3", "2");
graph.addEdge("4", "3");


DirectedGraphWeighted wgraph = new DirectedGraphWeighted();

wgraph.addNode("1");
wgraph.addNode("2");
wgraph.addNode("3");
wgraph.addNode("4");

wgraph.addEdge("1", "2", 0);
wgraph.addEdge("2", "3", 0);
wgraph.addEdge("2", "4", 0);
wgraph.addEdge("3", "2", 0);
wgraph.addEdge("4", "3", 0);

Boolean normalizeScores = false;

Dictionary<string, double> tokens = new PageRankMgr(wgraph).getPageRanks(normalizeScores);

foreach(var token in tokens)
{
    Console.WriteLine(token);
}
