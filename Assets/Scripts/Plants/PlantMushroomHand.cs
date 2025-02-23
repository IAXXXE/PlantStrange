using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlantMushroomHand : Plant
{
    protected override void Skill()
    {
        base.potTrans.DOShakeScale(1f, 1, 5);
        Delegation.CallBugsStatChange(BugsStat.Sleep);
    }
}
