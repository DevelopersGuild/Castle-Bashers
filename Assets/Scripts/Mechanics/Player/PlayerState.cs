using UnityEngine;
using System.Collections;

public interface IPlayerState
{
    IPlayerState HandleInput(Player player, Input input);
    void UpdateState(Player player);
    void EnterState(Player player);
}
