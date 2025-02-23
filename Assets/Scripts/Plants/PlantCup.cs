using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantCup : Plant
{
    protected override void Skill()
    {

        base.potTrans.DOShakeScale(1f, 1, 3);
        
    }
}
