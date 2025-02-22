using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalE : Enemy
{
    public override void GetParams()
    {
        HP = GLOBAL.NORMALHP;
        SpeedCoeff = GLOBAL.HIGHSP;
        Damage = GLOBAL.HIGHDM;
    }
}
