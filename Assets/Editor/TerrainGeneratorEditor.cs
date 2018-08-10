using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = target as TerrainGenerator;

        if(DrawDefaultInspector())
        {
            terrainGenerator.GenerateTerrain();
        }

        if(GUILayout.Button("Generate"))
        {
            terrainGenerator.GenerateTerrain();
        }
    }
}
