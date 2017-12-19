﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare
{
    public GameObject mSquare = null;
    public GameObject mContents = null;
    public float mRemainingMovement = 0;
    public bool mVisited = false;
    public IntVec2 mPositionRef { get { return mSquare.GetComponent<OnGrid>().mPosition; } set { mSquare.GetComponent<OnGrid>().mPosition = value; } }

    public void PathfindingReset()
    {
        mRemainingMovement = 0;
        mVisited = false;
    }
}

public class GridLogic : MonoBehaviour {

    GridSquare[,] mGridSqiares;

    // real stuff
    public GameObject mGridSquarePrefab;
    public int mXSize;
    public int mYSize;

    // testing vars
    public int counter = 0;
    public GameObject mThingToPlace;
    public Vector2 mPlaceToPutIt;
    // testing functions

     // real functions
    public Vector3 BecomeIntegers(Vector3 vec)
    {
        for(int i = 0; i < 3; ++i)
        {
            vec[i] = (int)vec[i];
        }
        return vec;
    }

    private GridSquare mHovered;

    // Use this for initialization
    public void Start () {
        mGridSqiares = new GridSquare[mXSize, mYSize];

        for (int x = 0; x < mXSize; ++x)
        {
            for (int y = 0; y < mYSize; ++y)
            {
                mGridSqiares[x,y] = new GridSquare();
                mGridSqiares[x,y].mSquare = Instantiate(mGridSquarePrefab, new Vector3(x, y, transform.position.z), new Quaternion());
                mGridSqiares[x,y].mSquare.GetComponent<OnGrid>().mPosition = new IntVec2(x, y);
            }
        }

    }

    public void Update()
    {
        CheckHoveredGridSquare();
    }

    bool InBounds(IntVec2 pos)
    {
        return pos.x >= 0 && pos.x < mXSize && pos.y >= 0 && pos.y < mYSize;
    }

    private void MatchObjectLocationToGridLocation(IntVec2 place)
    {
        GameObject moveable = mGridSqiares[place.x, place.y].mContents;
        if (moveable)
            moveable.transform.position = new Vector3(place.x, place.y, -(moveable.transform.localScale.z / 2) - 1);
    }

    public void PlaceOnGrid(GameObject thingToPlace, IntVec2 place)
    {
        place.x = Mathf.Clamp(place.x, 0, mXSize - 1);
        place.y = Mathf.Clamp(place.y, 0, mYSize - 1);

        if (thingToPlace.GetComponent<OnGrid>().mPosition.x != -1337)
            RemoveFromGrid(thingToPlace);

        thingToPlace.GetComponent<OnGrid>().mPosition = place;

        if (mGridSqiares[place.x, place.y].mContents != null)
            Debug.LogWarning("placed something in a full space");

        mGridSqiares[place.x, place.y].mContents = thingToPlace;
        MatchObjectLocationToGridLocation(place);
    }

    public void RemoveFromGrid(GameObject thingToRemove)
    {
        IntVec2 place = thingToRemove.GetComponent<OnGrid>().mPosition;
        thingToRemove.GetComponent<OnGrid>().mPosition.x = -1337;
        thingToRemove.GetComponent<OnGrid>().mPosition.y = -1337;
        mGridSqiares[place.x,place.y].mContents = null;
    }

    public void SwapGridContents(IntVec2 pos1, IntVec2 pos2)
    {
        GameObject temp = mGridSqiares[pos1.x, pos1.y].mContents;
        mGridSqiares[pos1.x, pos1.y].mContents = mGridSqiares[pos2.x, pos2.y].mContents;
        mGridSqiares[pos2.x, pos2.y].mContents = temp;

        if(mGridSqiares[pos1.x, pos1.y].mContents)
        {
            mGridSqiares[pos1.x, pos1.y].mContents.GetComponent<OnGrid>().mPosition = pos1;
        }

        if (mGridSqiares[pos2.x, pos2.y].mContents)
        {
            mGridSqiares[pos2.x, pos2.y].mContents.GetComponent<OnGrid>().mPosition = pos2;
        }

        MatchObjectLocationToGridLocation(pos1);
        MatchObjectLocationToGridLocation(pos2);
    }

    public GameObject GetGridContents(IntVec2 pos)
    {
        if (!InBounds(pos))
        {
            Debug.LogWarning("out of bounds grid content fetch attempted");
            return null;
        }
        return mGridSqiares[pos.x, pos.y].mContents;
    }

    public GridSquare GetGridSquare(IntVec2 pos)
    {
        if (!InBounds(pos))
        {
            //Debug.LogWarning("out of bounds grid square fetch attempted");
            return null;
        }
        return mGridSqiares[pos.x, pos.y];
    }

    public void ResetPathfindingData()
    {
        for (int x = 0; x < mXSize; ++x)
        {
            for (int y = 0; y < mYSize; ++y)
            {
                mGridSqiares[x, y].PathfindingReset();
            }
        }
    }

    private void CheckHoveredGridSquare()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mHovered = null;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask: LayerMask.GetMask("GridSquare")))
        {
            IntVec2 pos = hit.transform.gameObject.GetComponent<OnGrid>().mPosition;
            mHovered = GetGridSquare(pos);
        }
    }

    public GridSquare GetHoveredGridSquare()
    {
        return mHovered;
    }

}