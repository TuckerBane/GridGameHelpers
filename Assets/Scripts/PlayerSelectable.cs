using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateFunction();
public delegate bool SquareEvaluator(GridSquare square, GridSquare prevSquare);

public enum PlayerID {PlayerOne, PlayerTwo, PlayerThree, PlayerFour, EnemyPlayer };

// No data. Would be a namespace in C++
public class Disqualifiers
{
    // mostly here for testing/proof of concept
    public static bool Distance(GridSquare square, GridSquare prevSquare)
    {
        return prevSquare.mRemainingMovement <= 0;
    }

    public static bool Visited(GridSquare square, GridSquare prevSquare)
    {
        return square.mVisited;
    }

    public static bool Diagonal(GridSquare square, GridSquare prevSquare)
    {
        return square.mPositionRef.x != prevSquare.mPositionRef.x && square.mPositionRef.y != prevSquare.mPositionRef.y;
    }

    public static bool Taken(GridSquare square, GridSquare prevSquare)
    {
        return square.mContents != null;
    }
}

public class PlayerSelectable : MonoBehaviour {

    List<SquareEvaluator> mDisqualifiers;
    GridLogic mGridRef;

    IntVec2 mPositionRef { get { return GetComponent<OnGrid>().mPosition; } set { GetComponent<OnGrid>().mPosition = value; } }
    public int mSpeed = 3;
    public bool mTurnTaken = false;
    public PlayerID mPlayerID;

    List<GridSquare> mMoveOptions;

    private void Start()
    {
        mMoveOptions = new List<GridSquare>();
        mGridRef = FindObjectOfType<GridLogic>();
        mDisqualifiers = new List<SquareEvaluator>();
        mDisqualifiers.Add(Disqualifiers.Distance);
        mDisqualifiers.Add(Disqualifiers.Visited);
        mDisqualifiers.Add(Disqualifiers.Diagonal);
        mDisqualifiers.Add(Disqualifiers.Taken);
    }

    private bool IsQualified(GridSquare square, GridSquare prevSquare)
    {
        foreach(var disQualifier in mDisqualifiers)
        {
            if (disQualifier(square, prevSquare))
                return false;
        }
        return true;
    }

    void AddMoveOption(GridSquare square)
    {
        square.mSquare.GetComponent<Highlightable>().setHighlighted(true);
        mMoveOptions.Add(square);
        // remember this and let you click on it
    }

    void PlayerSelectedUpdate(PlayerTurn playerTurn)
    {
        if (mTurnTaken)
        {
            // deselect this unit
            playerTurn.mSelectedEntity = null; 
            // LATER provide feedback for what happened
            return;
        }
        if (mSpeed < 0)
            return;

        mMoveOptions.Clear();

        // LATER convert to a priority queue to allow variable movement costs
        Queue<GridSquare> placesToVisit = new Queue<GridSquare>();
        placesToVisit.Enqueue( mGridRef.GetGridSquare(gameObject.GetComponent<OnGrid>().mPosition) );
        placesToVisit.Peek().mRemainingMovement = mSpeed;
        placesToVisit.Peek().mVisited = true;
        while(placesToVisit.Count != 0)
        {
            var oldSquare = placesToVisit.Dequeue();
            for(int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    var newSquare = mGridRef.GetGridSquare(oldSquare.mPositionRef + new IntVec2(x, y));
                    if (newSquare != null && IsQualified(newSquare, oldSquare) )
                    {
                        newSquare.mRemainingMovement = oldSquare.mRemainingMovement - 1;
                        newSquare.mVisited = true;
                        AddMoveOption(newSquare);
                        placesToVisit.Enqueue(newSquare);
                    }
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var grid = GameObject.FindObjectOfType<GridLogic>();
            var hoveredGridSquare = grid.GetHoveredGridSquare();
            if (hoveredGridSquare != null && mMoveOptions.Contains(hoveredGridSquare))
            {
                grid.PlaceOnGrid(gameObject, hoveredGridSquare.mPositionRef);
                // deselect this unit
                playerTurn.DeselectAll();
                mTurnTaken = true;
            }
        }

        mGridRef.ResetPathfindingData();
    }

}
