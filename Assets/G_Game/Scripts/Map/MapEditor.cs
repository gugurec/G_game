using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapCreator map = (MapCreator)target;

        if (GUILayout.Button("Create map"))
        {
            map.CreateMap();
        }
        if (GUILayout.Button("Clear map"))
        {
            map.ClearMap();
        }
    }
}
