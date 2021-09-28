using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    private CharactersData data;
    public Tilemap tilemap;
    public GameArea gameArea;

    void Start()
    {
        tilemap.CompressBounds();
        gameArea = new GameArea(tilemap);
        data = GameObject.Find("CharacterData").GetComponent<CharactersData>();
        foreach (character c in data.characterList)
        {
            gameArea.setCharacterOfANode(c.location, Instantiate(c.prefab, tilemap.GetCellCenterWorld(gameArea.getPositionOfALocation(c.location)), Quaternion.identity));
            gameArea.getCharacterOfANode(c.location).GetComponent<CharacterMovement>().gameArea = gameArea;
            gameArea.getCharacterOfANode(c.location).GetComponent<CharacterMovement>().location = c.location;
            gameArea.getCharacterOfANode(c.location).tag = c.team.ToString();
        }
    }
}
