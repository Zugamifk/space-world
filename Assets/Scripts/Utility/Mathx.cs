using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathx {
    public static Vector2 Rotate(this Vector2 vector2, float angle)
    {
        var theta = angle*Mathf.Deg2Rad;
        var cs = Mathf.Cos(theta);
        var sn = Mathf.Sin(theta);
        var x = vector2.x;
        var y = vector2.y;
        var px = x * cs - y * sn;
        var py = x * sn + y * cs;
        return new Vector2(px, py);
    }
}
