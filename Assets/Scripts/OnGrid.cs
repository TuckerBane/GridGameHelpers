using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrid : MonoBehaviour {

    public IntVec2 mPosition = new IntVec2 ( -1337, -1337 );

    public IntVec2 GetPosition()
    {
        if (mPosition.x == -1337 || mPosition.y == -1337)
            Debug.LogError("OnGrid position uninitilized!");
        return mPosition;
    }

}
