using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public struct IntVector2
    {
        public int x, y;

        public IntVector2(int[] xy)
        {
            x = xy[0];
            y = xy[1];
        }

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static class Vector2Extensions
    {
        public static IntVector2 ToIntVector2(this Vector2 vector2)
        {
            int[] intVector2 = new int[2];
            for (int i = 0; i < 2; ++i) intVector2[i] = Mathf.RoundToInt(vector2[i]);
            return new IntVector2(intVector2);
        }
    }
}
