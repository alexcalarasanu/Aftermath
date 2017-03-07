using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    int width;
    int height;

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
        Debug.Log("World Created, width= " + width + " , height= " + height);
    }

    public Tile GetTileAt(int x, int y)
    {
        if( x > Width || x < 0 || y > Height || y < 0)
        {
            Debug.LogError("TIle out of bounds");
            return null;
        }
        return tiles[x, y];
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].Type = Tile.TileType.Dirt;
            }
        }
        Debug.Log("Tiles generated");
    }

}
