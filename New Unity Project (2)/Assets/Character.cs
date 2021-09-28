using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character
{
    public int team;     
    public Vector2Int location;  
    public GameObject prefab;
    public character(int team, Vector2Int location, GameObject prefab)
    {
        this.team = team;        
        this.location = location;        
        this.prefab = prefab;
    }
}

