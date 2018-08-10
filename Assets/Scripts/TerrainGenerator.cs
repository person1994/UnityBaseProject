using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour {

    private Terrain terrain;

    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (height < 1)
        {
            height = 1;
        }
        if (scale <= 0f)
        {
            scale = 0.000001f;
        }
    }

    public void GenerateTerrain()
    {
        if (!terrain)
        {
            terrain = GetComponent<Terrain>();
        }

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        OnValidate();

        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for(int x=0; x <width; x++)
        {
            for(int y=0; y<height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
