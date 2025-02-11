using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    public GameObject Supports;
    protected override void Turning()
    {
        Vector3 TargetPos2 = TargetPos;
        TargetPos2.y = 0;
        Quaternion lastPos2 = Quaternion.LookRotation(-TargetPos2);
        Supports.transform.rotation = Quaternion.Slerp(Supports.transform.rotation, lastPos2, Time.deltaTime * turnSpeed);
        base.Turning();
    }
}
