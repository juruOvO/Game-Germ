using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalB : Enemy
{
    public override void GetParams()
    {
        HP = GLOBAL.MEDIUMHP;
        SpeedCoeff = GLOBAL.NORMALSP;
        Damage = GLOBAL.NORMALDM;
    }
}
