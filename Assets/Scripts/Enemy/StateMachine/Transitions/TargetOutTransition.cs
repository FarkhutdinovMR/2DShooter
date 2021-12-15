using UnityEngine;

public class TargetOutTransition : Transition
{
    [SerializeField] private float _outDistance;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) > _outDistance)
            NeedTransit = true;
    }
}