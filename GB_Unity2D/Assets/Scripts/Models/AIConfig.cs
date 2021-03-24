using System;
using UnityEngine;

[Serializable]
public struct AIConfig
{
    public float speed;
    public float minSqrDistanceToTarget;
    public Transform[] waypoints;
}