using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [SerializeField]
    private GameObject characterPrefab;
    private List<Character> characters = new List<Character>();

    public void SpawnCharacter(Vector3 pos)
    {
        var characterGO = Instantiate(characterPrefab, transform);
        var character = characterGO.GetComponent<Character>();
        if (character)
        {
            characters.Add(character);
            character.MoveToPos(pos);
        }
        else
        {
            Debug.LogError("Not found Character in characterPrefab");
        }
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
