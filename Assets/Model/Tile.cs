using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
    {

    public enum TileType { Dirt, Shallow_Water };

    Action<Tile> tileTypeChanged_callback;

    private TileType type = TileType.Dirt;
    public TileType Type
    {
        get { return type; }
        set
        {
            type = value;
            
            if (tileTypeChanged_callback != null)
                tileTypeChanged_callback(this);
        }
    }


    LooseObject looseObject;
    InstalledObject installedObject;

    World world;
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public Tile( World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;
    }
    
    public void OnTileTypeChangedCB(Action<Tile> cb)
    {
        tileTypeChanged_callback = cb;
    }
}
