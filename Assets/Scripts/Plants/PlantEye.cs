using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantEye : Plant
{
    protected override void Skill()
    {
        base.potTrans.DOShakeScale(1f, 1, 5);
        Delegation.CallSpawnBugs(5);
    }
}
