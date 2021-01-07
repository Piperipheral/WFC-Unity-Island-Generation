using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCStarter : MonoBehaviour
{
    [Header("Data Slots")]
    public GameObject slotPrefab;
    [Header("Gen Parameters")]
    public Vector3Int genSize;

    [Header("Runtime Data")]
    public Slot[,,] slots;
    public Corner[,,] corners;
    [Header("Visuals")]
    public Color cornerColorOn = Color.green;
    public Color cornerColorOff = Color.gray;

    //miscs
    private Vector3 offsetGizmo = new Vector3(0.5f, 0.5f, 0.5f);
    void Start()
    {
        InitializeValues();

    }
    void InitializeValues()
    {
        //setup WFCMain
        WFCMain.slotPrefab = slotPrefab;
        corners = WFCMain.CreateCorners(genSize, transform);
        slots = WFCMain.CreateSlots(genSize, corners, transform);
        Debug.Log("Number of corners: " + corners.GetLength(0) * corners.GetLength(1) * corners.GetLength(2));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        if (corners != null)
            foreach (Corner c in corners)
            {
                Gizmos.color = c.isActive ? cornerColorOn : cornerColorOff;
                Gizmos.DrawSphere(c.myPosition - offsetGizmo, 0.1f);
            }
    }
}
