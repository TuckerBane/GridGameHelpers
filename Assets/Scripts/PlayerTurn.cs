using UnityEngine;
using System.Collections;

public class PlayerTurn : Turn
{
    public PlayerTurn( UnitInfo units, PlayerID player) : base("Player turn") { mMyUnits = units; mPlayerID = player; }

    public UnitInfo mMyUnits;
    PlayerID mPlayerID;

    GameObject mSelectedSquare = null;
    public GameObject mSelectedEntity = null;

    public override bool Update()
    {
        if(mSelectedEntity)
        {
            mSelectedEntity.SendMessage("PlayerSelectedUpdate", this);
            return false;
        }
        // if all the units have moved the turn is over (good enough for now)
        if (mMyUnits.mNextInTurnOrder == null)
            return true;

        var grid = GameObject.FindObjectOfType<GridLogic>();
        GameObject hovered = null;
        if(grid.GetHoveredGridSquare() != null)
            hovered = grid.GetHoveredGridSquare().mSquare;

        if (hovered && hovered.GetComponent<Highlightable>())
            hovered.GetComponent<Highlightable>().SetHighlighted(true, 0.01f);

        if (Input.GetKeyUp(KeyCode.Mouse0) && hovered && hovered.GetComponent<OnGrid>()) {
            if (hovered)
            {
                DeselectAll();
                mSelectedSquare = hovered;
                IntVec2 selectedPos = mSelectedSquare.GetComponent<OnGrid>().GetPosition();
                mSelectedEntity = grid.GetGridContents(selectedPos);
                if (mSelectedEntity && mSelectedEntity.GetComponent<PlayerSelectable>() && mSelectedEntity.GetComponent<PlayerSelectable>().mPlayerID == mPlayerID)
                {
                    if (mSelectedSquare.GetComponent<Highlightable>())
                        mSelectedSquare.GetComponent<Highlightable>().SetHighlighted(true, float.MaxValue);
                }
                else
                {
                    DeselectAll();
                }

            }
        }

        return false;
    }

    public override void Reset()
    {
        mMyUnits.RefreshUnits();
        mSelectedEntity = null;
        mSelectedSquare = null;
    }

    public void DeselectAll()
    {
        if (mSelectedSquare && mSelectedSquare.GetComponent<Highlightable>())
            mSelectedSquare.GetComponent<Highlightable>().SetHighlighted(false);
        mSelectedSquare = null;
        mSelectedEntity = null;
    }
};
