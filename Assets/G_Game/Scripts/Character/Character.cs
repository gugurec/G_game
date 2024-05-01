using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> characterVisual;
    [SerializeField]
    private SpriteRenderer visual;

    public Character()
    {
    }
    public void MoveToPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
