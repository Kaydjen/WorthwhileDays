using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldGrid 
{
    public static Vector3 GridPositionFromWorldPoint3D(Vector3 worldPos, float gridScale)
    {
        var x = MathF.Round(worldPos.x / gridScale) * gridScale;
        var y = MathF.Round(worldPos.y / gridScale) * gridScale;
        var z = MathF.Round(worldPos.z / gridScale) * gridScale;

        return new Vector3(x, y, z);
    }
}
