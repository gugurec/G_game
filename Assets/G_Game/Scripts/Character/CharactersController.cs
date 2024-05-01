using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map;

public class CharactersController : MonoBehaviour
{
    [SerializeField]
    private GameObject characterPrefab;
    private List<Character> characters = new List<Character>();
    private Character currentControlledCharacter;
    public void SpawnCharacter(Map.MapPos mapPos)
    {
        var characterGO = Instantiate(characterPrefab, transform);
        var character = characterGO.GetComponent<Character>();
        if (character)
        {
            characters.Add(character);
            character.MoveToPos(mapPos);
        }
        else
        {
            Debug.LogError("Not found Character in characterPrefab");
        }
    }
    public void MoveCurrentCharacterToPos(Map.MapPos mapPos)
    {
        currentControlledCharacter.MoveToPos(mapPos);
    }
    public bool TrySelecCharacter(Map.MapPos mapPos)
    {
        foreach (var character in characters)
        {
            if(character.CurrentCellPos == mapPos.cellPos)
            {
                currentControlledCharacter = character;
                character.Select();
                return true;
            }
        }
        return false;
    }
    public void UnSelect()
    {
        foreach (var character in characters)
        {
            character.UnSelect();
        }
        currentControlledCharacter = null;
    }
    public bool IsHaveControlledCharacter()
    {
        return currentControlledCharacter != null;
    }
    private void RemoveCharacters()
    {
        foreach (var character in characters)
        {
            Destroy(character.gameObject);
        }
        characters.Clear();
    }
}
