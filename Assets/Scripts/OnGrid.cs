using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrid : MonoBehaviour {

    private GridLogic mMyGrid;

    private void Start()
    {
        mMyGrid = FindObjectOfType<GridLogic>();
    }

    [SerializeField]
    private IntVec2 mRealPosition;

    public IntVec2 mPosition
    {
        get{ return mRealPosition; }
        set{ mMyGrid.PlaceOnGrid(gameObject, value); }
    }

    // only for use in GridLogic
    public void HardSetPosition(IntVec2 val)
    {
        mRealPosition = val;
    }

    public IntVec2 GetPosition()
    {
        if (mPosition.x == -1337 || mPosition.y == -1337)
            Debug.LogError("OnGrid position uninitilized!");
        return mPosition;
    }

}
