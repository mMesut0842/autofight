using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector2Int location;
    public GameArea gameArea;
    public float health = 100;
    private float attackdelay = 0;

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        Stack<int> target = FindTarget();
        if(target.Count != 0)
        {
            if (gameObject.transform.position == gameArea.tilemap.GetCellCenterWorld(gameArea.getPositionOfALocation(location)))
            {
                target.Pop();
                if(gameArea.getCharacterOfANode(target.Peek()) == null)
                {
                    gameArea.setCharacterOfANode(location, null);
                    location = gameArea.getLocationOfAnIndex(target.Pop());
                    gameArea.setCharacterOfANode(location, gameObject);
                }
                else
                {
                    if (attackdelay <= 0 && gameArea.getCharacterOfANode(target.Peek()).transform.position == gameArea.tilemap.GetCellCenterWorld(gameArea.getPositionOfALocation(gameArea.getLocationOfAnIndex(target.Peek()))))
                    {
                        gameArea.getCharacterOfANode(target.Pop()).GetComponent<CharacterMovement>().health -= 20;
                        attackdelay = 2;
                    }
                    else
                    {
                        attackdelay -= Time.deltaTime;
                    }
                }
            }
            else 
            {
                gameObject.transform.position += FindDirection() * 0.001f;
                gameObject.transform.position = new Vector3((float)Mathf.Round(gameObject.transform.position.x * 1000f) / 1000f, (float)Mathf.Round(gameObject.transform.position.y * 1000f) / 1000f, gameObject.transform.position.z);

            }
        }
    }

    Stack<int> FindTarget()
    {
        Stack<int> path = new Stack<int>();
        Dictionary<int, int> previousNode = new Dictionary<int, int>();
        List<int> visited = new List<int>();
        Queue<int> nodeQueue = new Queue<int>();

        nodeQueue.Enqueue(gameArea.getIndex(location));
        visited.Add(gameArea.getIndex(location));
        while (nodeQueue.Count > 0)
        {
            foreach(int edge in gameArea.getEdgesOfANode(nodeQueue.Peek()))
            {
                if (!visited.Contains(edge) && (gameArea.getCharacterOfANode(edge) == null || !gameArea.getCharacterOfANode(edge).CompareTag(gameObject.tag)))
                {
                    previousNode.Add(edge, nodeQueue.Peek());
                    if (gameArea.getCharacterOfANode(edge) != null && !gameArea.getCharacterOfANode(edge).CompareTag(gameObject.tag))
                    {
                        int index = edge;
                        path.Push(index);
                        do
                        {
                            path.Push(previousNode[index]);
                            index = previousNode[index];
                        } while (index != gameArea.getIndex(location));
                        return path;

                    }
                    nodeQueue.Enqueue(edge);
                    visited.Add(edge);
                }
            }
            nodeQueue.Dequeue();
        }

        return new Stack<int>();
    }

    Vector3 FindDirection()
    {
        Vector3 position = gameArea.tilemap.GetCellCenterWorld(gameArea.getPositionOfALocation(location));
        return new Vector3(
            (position.x - gameObject.transform.position.x) / Mathf.Abs(position.x - gameObject.transform.position.x + position.y - gameObject.transform.position.y),
            (position.y - gameObject.transform.position.y) / Mathf.Abs(position.y - gameObject.transform.position.y + position.x - gameObject.transform.position.x), 0);
    }
}
