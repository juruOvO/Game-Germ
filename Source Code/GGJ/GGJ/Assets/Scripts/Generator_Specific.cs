using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Intensity { 
    LOW,MEDIUM, HIGH
}

public class Generator_Specific : Generator
{
    Intensity intensity;
    public override void SetIntensity()
    {
        intensity = Intensity.LOW;
    }
    public override void SetParams()
    {
        switch (intensity)
        {
            case Intensity.LOW:
                generateBaseNum = GLOBAL.LOWBN;
                intervalTime = GLOBAL.LOWIT;
                sessionTime = GLOBAL.LOWST;
                breakTime = GLOBAL.LOWBT;
                climaxTime = GLOBAL.LOWCT;
                startTime = GLOBAL.LOWSTT;
                break;
            case Intensity.MEDIUM:
                generateBaseNum = GLOBAL.MIDBN;
                intervalTime = GLOBAL.MIDIT;
                sessionTime = GLOBAL.MIDST;
                breakTime = GLOBAL.MIDBT;
                climaxTime = GLOBAL.MIDCT;
                startTime = GLOBAL.MIDSTT;
                break;
            case Intensity.HIGH:
                generateBaseNum = GLOBAL.HIGBN;
                intervalTime = GLOBAL.HIGIT;
                sessionTime = GLOBAL.HIGST;
                breakTime = GLOBAL.HIGBT;
                climaxTime = GLOBAL.HIGCT;
                startTime = GLOBAL.HIGSTT;
                break;
        }
    }
}
