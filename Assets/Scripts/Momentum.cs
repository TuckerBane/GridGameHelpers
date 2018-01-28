using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnGrid))]
public class Momentum : MonoBehaviour {
    //clockwise order
    IntVec2[] mDirections = {new IntVec2(0,1), new IntVec2(1, 1), new IntVec2(1, 0), new IntVec2(1, -1),
                            new IntVec2(0,-1),new IntVec2(-1,-1), new IntVec2(-1,0), new IntVec2(-1,1),       };
    int GetDirectionIndex(IntVec2 direction)
    {
        for(int i = 0; i < 8; ++i)
        {
            if (mDirections[i] == direction)
                return i;
        }
        return -1;
    }


    public IntVec2 mVelocity;
    public bool mApplyVelocityButton = false;
    private GridLogic mGridLogicRef;

    private OnGrid mMyOnGrid;
    private IntVec2 mRawVelocityLeftToApply;
    private IntVec2 mVelocityLeftToApply
    {
        get { return mRawVelocityLeftToApply; }
        set
        {
            mRawVelocityLeftToApply = value;
            delta.x = mVelocityLeftToApply.x == 0 ? 0 : mVelocityLeftToApply.x / Mathf.Abs(mVelocityLeftToApply.x);
            delta.y = mVelocityLeftToApply.y == 0 ? 0 : mVelocityLeftToApply.y / Mathf.Abs(mVelocityLeftToApply.y);
        }
    }
    private IntVec2 delta;

    // Use this for initialization
    void Start () {
        mMyOnGrid = GetComponent<OnGrid>();
        mGridLogicRef = FindObjectOfType<GridLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        if(mApplyVelocityButton)
        {
            mApplyVelocityButton = false;
            ApplyMomentum();
        }

    }

    private IntVec2 baseVelocityDelta = new IntVec2();

    void ApplyMomentum()
    {
        baseVelocityDelta.x = mVelocity.x == 0 ? 0 : mVelocity.x / Mathf.Abs(mVelocity.x);
        baseVelocityDelta.y = mVelocity.y == 0 ? 0 : mVelocity.y / Mathf.Abs(mVelocity.y);

        mVelocityLeftToApply = mVelocity;
        IntVec2 nextPos = mMyOnGrid.mPosition;
        do
        {
            mMyOnGrid.mPosition = nextPos;
            nextPos = GetNextSquare();
            // for now bounce off all things equally. Maybe put this logic in GetNextSquare
            // LATER be able to bounce at an angle
            /*
            while (nextPos != IntVec2.Invalid && mGridLogicRef.GetGridSquare(nextPos).mContents != null)
            {
                // slow down after you run into things
                mVelocity -= baseVelocityDelta;
                // reverse the direction of everything

                baseVelocityDelta *= -1;
                mVelocityLeftToApply *= -1;
                mVelocity *= -1;
                nextPos = GetNextSquare();
            }
            */
        } while (nextPos != IntVec2.Invalid);
    }

    void ChangeDirection(IntVec2 newDirection)
    {
        // slow down after you run into things
        mVelocity -= baseVelocityDelta;
        // reverse the direction of everything

        if(delta.x != newDirection.x)
        {
            baseVelocityDelta.x *= -1;
            mVelocityLeftToApply = new IntVec2(-mVelocityLeftToApply.x, mVelocityLeftToApply.y);
            mVelocity.x *= -1;
        }
        if (delta.y != newDirection.y)
        {
            baseVelocityDelta.y *= -1;
            mVelocityLeftToApply = new IntVec2(mVelocityLeftToApply.x, -mVelocityLeftToApply.y);
            mVelocity.y *= -1;
        }

    }

    private int mRealDirectionIndex;
    private int mDirectionIndex
    {
        get { return mRealDirectionIndex; }
        set { mRealDirectionIndex = value % 8; }
    }

    IntVec2 GetNextSquare()
    {
        if (mVelocityLeftToApply == new IntVec2(0, 0))
            return IntVec2.Invalid;

        IntVec2 result = mMyOnGrid.mPosition + delta;

        if(mGridLogicRef.GetGridSquare(result).mContents != null)
        {

            mDirectionIndex = GetDirectionIndex(delta);
            // diagonal
            if(mDirectionIndex % 2 == 1)
            {
                bool clockwiseBlocked = mGridLogicRef.GetGridSquare(mMyOnGrid.mPosition + mDirections[ (mDirectionIndex + 1) % 8]).mContents != null;
                bool counterClockwiseBlocked = mGridLogicRef.GetGridSquare(mMyOnGrid.mPosition + mDirections[ (mDirectionIndex - 1) % 8]).mContents != null;
                if (clockwiseBlocked == counterClockwiseBlocked) // both or neither
                    mDirectionIndex += 4; // uturn
                else if (clockwiseBlocked)
                    mDirectionIndex -= 2; // 90 degree away from wall
                else
                    mDirectionIndex += 2; // 90 degree away from wall

                // change facing


            }
            else // not diaginal
            {
                mDirectionIndex += 4; // 180 degree turn
            }
            ChangeDirection(mDirections[mDirectionIndex]);
            return GetNextSquare(); // try again with new direction
        }

        mVelocityLeftToApply -= delta;
        return result;
    }

}
