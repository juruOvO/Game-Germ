using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalD : Enemy
{
    public override void GetParams()
    {
        HP = GLOBAL.HIGHHP;
        SpeedCoeff = GLOBAL.NORMALSP;
        Damage = GLOBAL.NORMALDM;
    }
}