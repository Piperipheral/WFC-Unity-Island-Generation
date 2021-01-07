using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Corner
{
    public Vector3 myPosition;
    public string name;
    public bool isActive;
    public Corner(Vector3 _myPosition, bool _isActive = false)
    {
        myPosition = _myPosition;
        isActive = _isActive;
        name = "Corner " + _myPosition.x + "_" + _myPosition.y + "_" + _myPosition.z;
    }
}
