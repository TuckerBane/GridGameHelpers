using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct IntVec2
{
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;

    public IntVec2(int xnew, int ynew)
    { x = xnew; y = ynew; }

    public static implicit operator IntVec2(Vector2 rhs)
    {return new IntVec2((int)rhs.x, (int)rhs.y);}

    void Zero() { x = 0; y = 0; }

    void Set(float newx, float newy) { x = (int)newx; y = (int)newy; }

    
    public static IntVec2 operator-(IntVec2 me) { return new IntVec2(-me.x, -me.y); }
    
    public static IntVec2 operator -(IntVec2 lhs, IntVec2 rhs) { return new IntVec2(lhs.x - rhs.x, lhs.y - rhs.y); }

    public static IntVec2 operator +(IntVec2 lhs, IntVec2 rhs) { return new IntVec2(lhs.x + rhs.x, lhs.y + rhs.y); }

    public static IntVec2 operator *(IntVec2 lhs, int rhs) { return new IntVec2(lhs.x * rhs, lhs.y * rhs); }

    public static implicit operator Vector2(IntVec2 me) { return new Vector2(me.x, me.y); }

};

















