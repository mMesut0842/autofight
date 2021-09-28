using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameArea
{
    public Tilemap tilemap;
    private Graph graph = new Graph();
    public GameArea(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        graph.addNode();
        for (int y = 2; y <= tilemap.size.y; y++)
        {
            graph.addNode();
            graph.addEdge(getIndex(new Vector2Int(0, y - 2)), getIndex(new Vector2Int(0, y - 1)));
        }
        for (int x = 2; x <= tilemap.size.x; x++)
        {
            graph.addNode();
            for (int y = 2; y <= tilemap.size.y; y++)
            {
                graph.addNode();
                graph.addEdge(getIndex(new Vector2Int(x - 1, y - 2)), getIndex(new Vector2Int(x - 1, y - 1)));
                graph.addEdge(getIndex(new Vector2Int(x - 2, y - 1)), getIndex(new Vector2Int(x - 1, y - 1)));
            }
        }
    }
    public int getIndex(Vector2Int tileLocation)
    {
        return tilemap.size.y * tileLocation.x + tileLocation.y;
    }
    public void setCharacterOfANode(Vector2Int tileLocation, GameObject character)
    {
        graph.graph[getIndex(tileLocation)].character = character;
    }
    public List<int> getEdgesOfANode(int nodeIndex)
    {
        return graph.graph[nodeIndex].edges;
    }
    public GameObject getCharacterOfANode(Vector2Int tileLocation)
    {
        return graph.graph[getIndex(tileLocation)].character;
    }
    public GameObject getCharacterOfANode(int index)
    {
        return graph.graph[index].character;
    }
    public Vector3Int getPositionOfALocation(Vector2Int tileLocation)
    {
        return new Vector3Int(tileLocation.x + tilemap.cellBounds.x, tileLocation.y + tilemap.cellBounds.y, 0);
    }
    public Vector2Int getLocationOfAnIndex(int index)
    {
        return new Vector2Int(index / tilemap.cellBounds.size.y, index % tilemap.cellBounds.size.y);
    }
}
