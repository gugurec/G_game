using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TileObject
{
    [SerializeField]
    private List<Sprite> characterVisual;
    [SerializeField]
    private SpriteRenderer visual;
    [SerializeField]
    private Color selectedColor = Color.red;
    [SerializeField]
    private Color unselecterColor = Color.white;

    public void Select()
    {
        visual.color = selectedColor;
    }
    public void UnSelect()
    {
        visual.color = unselecterColor;
    }
}
