using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalA : Enemy
{
    public override void GetParams()
    {
        HP = GLOBAL.NORMALHP;
        SpeedCoeff = GLOBAL.NORMALSP;
        Damage = GLOBAL.NORMALDM;
    }
}
