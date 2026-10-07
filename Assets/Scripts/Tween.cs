using UnityEngine;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }
    
    public Tween(Transform target, Vector3 startPosition, Vector3 endPosition, float startTime, float duration)
    {
        Target = target;
        StartPos = startPosition;
        EndPos = endPosition;
        StartTime = startTime;
        Duration = duration;
    }
}


