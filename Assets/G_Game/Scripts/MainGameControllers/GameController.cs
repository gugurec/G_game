using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Map map;
    [SerializeField]
    private CharactersController charactersController;
    [SerializeField]
    private ChestsController chestsController;
    [SerializeField]
    CameraController cameraController;
    private GameState currentGameState;

    private enum GameState
    {
        None = 0,
        SelectCard,
        DiceRoll,
        SelectTileToMove
    }

    private void SpawnCharacter()
    {
        charactersController.SpawnCharacter(map);
    }
    private void SpawnChests()
    {
        chestsController.SpawnChests(map);
    }
    private void OnTileClick(Map.MapPos mapPos)
    {
        if (charactersController.IsHaveControlledCharacter())
        {
            charactersController.MoveCurrentCharacterToPos(mapPos);
            cameraController.MoveCamera(mapPos);
        }
        else
        {
            if (charactersController.TrySelecCharacter(mapPos))
            {
                map.AnimateMap(mapPos);
            }
        }
        chestsController.OpenChest(mapPos);
    }

    private void Start()
    {
        SpawnCharacter();
        SpawnChests();
    }

    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        map.OnTileClickEvent += OnTileClick;
    }
    private void Unsubscribe()
    {
        map.OnTileClickEvent -= OnTileClick;
    }
}
