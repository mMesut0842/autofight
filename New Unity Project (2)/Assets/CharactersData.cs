using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersData : MonoBehaviour
{

    public List<GameObject> prefabList;

    public List<character> characterList = new List<character>();

    private void Awake()
    {
        characterList.Add(new character(1, new Vector2Int(0, 1), prefabList[0]));
        characterList.Add(new character(2, new Vector2Int(3, 0), prefabList[0]));
        characterList.Add(new character(2, new Vector2Int(4, 3), prefabList[0]));

    }
}
