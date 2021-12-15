using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    public State NextState => _nextState;
    public bool NeedTransit { get; protected set; }

    protected Player Target { get; private set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Initialize(Player target)
    {
        Target = target;
    }
}