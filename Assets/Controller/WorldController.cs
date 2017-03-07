using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; protected set; }
    public World World { get; protected set; }
    public Sprite dirtSprite;
	// Use this for initialization
	void Start () {
        World = new World();
        if (Instance != null)
            Debug.LogError("Trying to create a second world controller");
        Instance = this; 

        //Create a GameObject for all the tiles

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                GameObject tile_go = new GameObject();
                Tile tile_data = World.GetTileAt(x, y);
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);

                tile_go.name = "Tile("+x+ "/"+y+")";
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.OnTileTypeChangedCB((tile) => { OnTileTypeChanged(tile, tile_go); });

               
            }
        }
        World.GenerateTiles();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if(tile_data.Type == Tile.TileType.Dirt)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = dirtSprite;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - not a valid tile type");
        }
    }
    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return World.GetTileAt(x, y);
    }
}
