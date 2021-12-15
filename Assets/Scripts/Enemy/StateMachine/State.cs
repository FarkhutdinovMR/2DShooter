using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    public event UnityAction Completed;

    protected Player Target;

    public void Enter(Player target)
    {
        Target = target;
        enabled = true;
        foreach (Transition transition in _transitions)
        {
            transition.enabled = true;
            transition.Initialize(target);
        }
    }

    public void Exit()
    {
        foreach (Transition transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public bool CheckTransition(out State nextState)
    {
        nextState = null;

        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                nextState = transition.NextState;
                break;
            }
        }

        return nextState != null;
    }

    protected void OnCompleted()
    {
        Completed?.Invoke();
    }
}