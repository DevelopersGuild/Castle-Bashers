using UnityEngine;
using System.Collections;

public interface IPlayerState
{
    IPlayerState HandleInput(Player player);
    void UpdateState(Player player);
    void EnterState(Player player);
}
