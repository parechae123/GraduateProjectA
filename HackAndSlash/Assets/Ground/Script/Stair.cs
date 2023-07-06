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
            waypoints[0] = stairCC.bounds.center;
            waypoints[1] = stairCC.bounds.ClosestPoint();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(waypoints[0], 1);
        Gizmos.DrawSphere(waypoints[1], 1);
    }
}
