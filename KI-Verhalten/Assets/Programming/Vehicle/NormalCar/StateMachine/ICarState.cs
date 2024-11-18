public interface ICarState
{
    void EnterState(CarStateMachine car);
    void ExitState();
    void UpdateState();
}
