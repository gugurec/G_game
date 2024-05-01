using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Map map;
    [SerializeField]
    private CharactersController charactersController;

    public void SpawnCharacter()
    {
        charactersController.SpawnCharacter(map.GetRandomTilePos());
    }
    private void Start()
    {
        SpawnCharacter();
    }
     
}
