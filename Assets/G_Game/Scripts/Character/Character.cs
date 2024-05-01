using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> characterVisual;
    [SerializeField]
    private SpriteRenderer visual;
    [SerializeField]
    private Color selectedColor = Color.red;
    [SerializeField]
    private Color unselecterColor = Color.white;
    [SerializeField] [ReadOnly]
    private Vector3Int currentCellPos;

    public void Select()
    {
        visual.color = selectedColor;
    }
    public void UnSelect()
    {
        visual.color = unselecterColor;
    }
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
