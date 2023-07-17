using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundState : MonoBehaviour
{
    public WhatIsThisFloor floorInfo;
    public Dictionary<Stair,Vector3> stairUPInfo = new Dictionary<Stair,Vector3>(); //Stair에는 Stair 컴포넌트를,Vector3에는 해당 계단의 위치값을 저장
    public Dictionary<Stair, Vector3> stairDownInfo = new Dictionary<Stair, Vector3>(); //Stair에는 Stair 컴포넌트를,Vector3에는 해당 계단의 위치값을 저장
    public RaycastHit hits;
    public Collider CD;
    public Vector3[] groundVertexPoint = new Vector3[4];
    public void Reset()
    {
        CD = GetComponent<Collider>();
        groundVertexPoint[0] = CD.bounds.min;
        groundVertexPoint[1] = groundVertexPoint[0]+Vector3.right*CD.bounds.size.x;
        while (Physics.Raycast(groundVertexPoint[0], Vector3.right, out hits, CD.bounds.size.x, 8))
        {
            if (hits.collider.TryGetComponent<Stair>(out Stair stairCompo))
            {
                stairDownInfo.Add(stairCompo, hits.collider.transform.position);
                Debug.Log(stairDownInfo.Count);
            }
            hits.collider.gameObject.SetActive(false);
        }
        groundVertexPoint[2] = groundVertexPoint[0] + Vector3.forward * CD.bounds.size.x;
        groundVertexPoint[3] = new Vector3(groundVertexPoint[1].x, groundVertexPoint[1].y, groundVertexPoint[2].z);
        
    }
}
[System.Serializable]
public enum WhatIsThisFloor
{
    underThird,underSecond,underFirst,first,second,third
}