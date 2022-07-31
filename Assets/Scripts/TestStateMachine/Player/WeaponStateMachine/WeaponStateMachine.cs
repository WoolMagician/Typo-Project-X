public class WeaponStateMachine
{
    public WeaponState CurrentState { get; private set; }
    public WeaponState PreviousState { get; private set; }

    public void Initialize(WeaponState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(WeaponState newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }
}