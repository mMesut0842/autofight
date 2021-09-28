using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Node> graph = new List<Node>();
    public void addNode()
    {
        graph.Add(new Node());
    }

    public void addEdge(int firstNode, int secondNode)
    {
        graph[firstNode].edges.Add(secondNode);
        graph[secondNode].edges.Add(firstNode);
    }
}

public class Node
{
    public GameObject character = null;
    public List<int> edges = new List<int>();
}
