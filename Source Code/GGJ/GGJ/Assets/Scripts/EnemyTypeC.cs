using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalC : Enemy
{
    public override void GetParams()
    {
        HP = GLOBAL.MEDIUMHP;
        SpeedCoeff = GLOBAL.MEDIUMSP;
        Damage = GLOBAL.MEDIUMDM;
    }
}
