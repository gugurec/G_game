using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsController : MonoBehaviour
{
    [SerializeField]
    private GameObject chestPrefab;
    private List<Chest> chests = new List<Chest>();

    public void SpawnChests(in Map map)
    {
        int count = Random.Range(1, 3);
        foreach (Map.MapPos mapPos in map.GetRandomTilesPos(count))
        {
            SpawnChest(mapPos);
        }
    }

    public void OpenChest(Map.MapPos mapPos)
    {
        foreach(Chest chest in chests)
        {
            if(chest.CurrentCellPos == mapPos.cellPos)
            {
                chest.Open();
            }
        }
    }

    private void SpawnChest(Map.MapPos mapPos)
    {
        var chestGO = Instantiate(chestPrefab, transform);
        var chest = chestGO.GetComponent<Chest>();
        if (chest)
        {
            chests.Add(chest);
            chest.MoveToPos(mapPos);
        }
        else
        {
            Debug.LogError("Not found Chest in chestPrefab");
        }
    }
}
