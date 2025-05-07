public class StateMachine
{
    public IState currentState { get; private set; }

    public void ChangeState(IState newState)
    {
        if (currentState != null) currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
    public void OnUpdate()
    {
        if (currentState != null) currentState.OnUpdate();
    }
}