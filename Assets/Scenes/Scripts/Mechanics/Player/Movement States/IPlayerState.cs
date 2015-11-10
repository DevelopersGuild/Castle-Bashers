
public interface IPlayerState
{
    IPlayerState HandleInput(Player player);
    void EnterState(Player player);
    void UpdateState(Player player);
    void ExitState(Player player);
}
