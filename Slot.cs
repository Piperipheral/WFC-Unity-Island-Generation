using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Slot : MonoBehaviour
{
    [Header("Visuals")]
    public bool renderGizmos = true;
    public Color boxColor = Color.blue;
    // Start is called before the first frame update
    private MeshRenderer _mr;
    [Header("Runtime Data")]
    public Vector3Int myCoords;
    public Corner[] corners;

    private bool[] _possibilitySpace;
    public bool isCollapsed;
    void Awake()
    {
        InitValues();
    }
    void InitValues()
    {
        _mr = GetComponent<MeshRenderer>();
        corners = new Corner[8];
        isCollapsed = false;
    }
    public void Initialize(Vector3Int _inVector)
    {
        myCoords = _inVector;
    }
    public void resetPossibilitySpace(int length = 0)
    {
        _possibilitySpace = new bool[length];
        for (int i = 0; i < length; i++) _possibilitySpace[i] = true;
    }
    public void collapseTile(int tileIndex)
    {
        for (int i = 0; i < _possibilitySpace.Length; i++)
        {
            _possibilitySpace[i] = (i == tileIndex);
        }
        isCollapsed = true;
    }
    public void setMyCorners(Corner[,,] _corners)
    {
        corners[0] = _corners[myCoords.x, myCoords.y, myCoords.z];
        corners[1] = _corners[myCoords.x + 1, myCoords.y, myCoords.z];
        corners[2] = _corners[myCoords.x, myCoords.y, myCoords.z + 1];
        corners[3] = _corners[myCoords.x + 1, myCoords.y, myCoords.z + 1];

        corners[4] = _corners[myCoords.x, myCoords.y + 1, myCoords.z];
        corners[5] = _corners[myCoords.x + 1, myCoords.y + 1, myCoords.z];
        corners[6] = _corners[myCoords.x, myCoords.y + 1, myCoords.z + 1];
        corners[7] = _corners[myCoords.x + 1, myCoords.y + 1, myCoords.z + 1];
    }
    private void OnDrawGizmos()
    {
        Vector3 boundsMan;
        if (renderGizmos)
        {
            Gizmos.color = boxColor;
            if (!_mr) boundsMan = GetComponent<MeshRenderer>().bounds.size;
            else boundsMan = _mr.bounds.size;
            Gizmos.DrawWireCube(transform.position, boundsMan);
        }
    }

    public bool[] getPossibilitySpace()
    {
        return _possibilitySpace;
    }
    public void setPossibilitySpace(bool[] _i)
    {
        _possibilitySpace = _i;
    }
    public int getCurrentlyPossibleTileCount()
    {
        int count = 0;
        for (int i = 0; i < _possibilitySpace.Length; i++)
        {
            count += (_possibilitySpace[i] ? 1 : 0);
        }
        return count;
    }
}
