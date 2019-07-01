using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void RotateAround(Transform t,Transform target,float angle)
    {
        t.RotateAround(target.position, target.up, angle);
    }
}
