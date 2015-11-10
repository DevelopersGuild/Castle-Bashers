using UnityEngine;
using System.Collections;

public interface ISkill
{
    void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0);
    float GetCoolDownTimer();
    int GetPrice();
    SkillLevel GetSkillLevel();
}
