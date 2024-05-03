using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : TileObject
{
    [SerializeField]
    private Sprite close;
    [SerializeField]
    private Sprite open;
    [SerializeField]
    private SpriteRenderer visual;

    public void Open()
    {
        visual.sprite = open;
    }

    public void Close()
    {
        visual.sprite = close;
    }
}
