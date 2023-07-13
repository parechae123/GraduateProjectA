using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public Vector3[] waypoints = new Vector3[2];
    private void Reset()
    {
        if(TryGetComponent<Collider>(out Collider stairCC))
        {
            waypoints[0] = stairCC.bounds.center-new Vector3(0,0,stairCC.bounds.extents.z);
            waypoints[1] = stairCC.bounds.center - new Vector3(0, 0, stairCC.bounds.extents.z);
        }

    }
    public Vector3 GetStairWayPoint(Vector3 plrPosition)
    {
        float tempWay;

        if (waypoints[0].x != waypoints[1].x)
        {
            tempWay = plrPosition.x;
            Mathf.Clamp(tempWay, waypoints[0].x, waypoints[1].x);
            return new Vector3(tempWay, plrPosition.y, waypoints[1].z);
        }
        else if (waypoints[0].z != waypoints[1].z)
        {
            tempWay = plrPosition.z;
            Mathf.Clamp(tempWay, waypoints[0].z, waypoints[1].z);
            return new Vector3(waypoints[1].x, plrPosition.y, tempWay);
        }
        return Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(waypoints[0], 1);
        Gizmos.DrawSphere(waypoints[1], 1);
    }
}
