using UnityEngine;

public class TileObject : MonoBehaviour, ITileObject
{
    [SerializeField]
    [ReadOnly]
    private Vector3Int currentCellPos;

    public void MoveToPos(Map.MapPos mapPos)
    {
        currentCellPos = mapPos.cellPos;
        transform.position = mapPos.worldPos;
    }

    public Vector3Int CurrentCellPos
    {
        get { return currentCellPos; }
    }
}
