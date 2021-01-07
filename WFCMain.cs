using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

}
public static class WFCMain
{
    [Header("Data")]
    public static GameObject slotPrefab;

    public static Tile[] tileDictionary;
    public static Slot[,,] CreateSlots(Vector3Int sizeMan, Corner[,,] corners, Transform parent = null)
    {
        Slot[,,] slotMan = new Slot[sizeMan.x, sizeMan.y, sizeMan.z];
        for (int _x = 0; _x < sizeMan.x; _x++)
        {
            for (int _y = 0; _y < sizeMan.y; _y++)
            {
                for (int _z = 0; _z < sizeMan.z; _z++)
                {
                    GameObject gMan = GameObject.Instantiate(slotPrefab);
                    gMan.transform.position = new Vector3(_x, _y, _z);
                    gMan.name = "Slot " + "_" + _x + "_" + _y + "_" + _z;
                    if (parent) gMan.transform.parent = parent;
                    slotMan[_x, _y, _z] = gMan.GetComponent<Slot>();
                    slotMan[_x, _y, _z].Initialize(new Vector3Int(_x, _y, _z));
                    slotMan[_x, _y, _z].setMyCorners(corners);
                }
            }
        }
        return slotMan;
    }
    public static Corner[,,] CreateCorners(Vector3Int sizeMan, Transform parent = null)
    {
        Corner[,,] corners = new Corner[sizeMan.x + 1, sizeMan.y + 1, sizeMan.z + 1];

        for (int _x = 0; _x <= sizeMan.x; _x++)
        {
            for (int _y = 0; _y <= sizeMan.y; _y++)
            {
                for (int _z = 0; _z <= sizeMan.z; _z++)
                {
                    corners[_x, _y, _z] = new Corner(new Vector3(_x, _y, _z), Random.Range(0, 100) > 50);
                }
            }
        }
        return corners;
    }

    public static void RunWFC(Slot[,,] slots)
    {
        PrepTiles(slots);
        //arbitrary pick tile at start
        Vector3Int coords = Vector3Int.zero;
        slots[coords.x, coords.y, coords.z].collapseTile(1);
        //propagate to adjacent tiles;
        PropagateTile(slots, coords);
    }
    public static void PropagateTile(Slot[,,] slots, Vector3Int tileCoord)
    {
        bool[] originTileePossibilitySpace = slots[tileCoord.x, tileCoord.y, tileCoord.z].getPossibilitySpace();
        int lowestEntropy = 99999;
        for (int _x = -1; _x < 1; _x++)
        {
            for (int _y = -1; _y < 1; _y++)
            {
                for (int _z = -1; _z < 1; _z++)
                {
                    if (_x != 0 && _y != 0 && _z != 0)
                    {
                        bool[] myTilePossibilitySpace = slots[tileCoord.x + _x, tileCoord.y + _y, tileCoord.z + _z].getPossibilitySpace();
                        for (int i = 0; i < tileDictionary.Length; i++)
                        {
                            if (myTilePossibilitySpace[i])
                            {
                                //check if tile is possible given the circumstances and set to forbidden accordingly
                            }
                        }
                        slots[tileCoord.x + _x, tileCoord.y + _y, tileCoord.z + _z].setPossibilitySpace(myTilePossibilitySpace);
                        lowestEntropy = Mathf.Min(lowestEntropy, slots[tileCoord.x + _x, tileCoord.y + _y, tileCoord.z + _z].getCurrentlyPossibleTileCount());
                    }
                }
            }
        }
    }
    private static void PrepTiles(Slot[,,] slots)
    {
        for (int _x = 0; _x < slots.GetLength(0); _x++)
        {
            for (int _y = 0; _y < slots.GetLength(1); _y++)
            {
                for (int _z = 0; _z < slots.GetLength(2); _z++)
                {
                    slots[_x, _y, _z].resetPossibilitySpace(tileDictionary.Length);
                }
            }
        }
    }
}
