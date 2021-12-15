using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _stopingDistance;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= _stopingDistance)
            NeedTransit = true;
    }
}