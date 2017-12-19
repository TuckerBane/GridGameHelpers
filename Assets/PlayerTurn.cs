using UnityEngine;
using System.Collections;

public class PlayerTurn : Turn
{
    public PlayerTurn() : base("Player turn") { }

    public IntVec2 mFirstSquare = new IntVec2(-1337, -1337);

    GameObject mSelectedSquare = null;
    public GameObject mSelectedEntity = null;

    public override bool Update()
    {
        if(mSelectedEntity)
        {
            mSelectedEntity.SendMessage("PlayerSelectedUpdate", this);
            return false;
        }

        var grid = GameObject.FindObjectOfType<GridLogic>();
        GameObject hovered = null;
        if(grid.GetHoveredGridSquare() != null)
            hovered = grid.GetHoveredGridSquare().mSquare;

        if (hovered && hovered.GetComponent<Highlightable>())
            hovered.GetComponent<Highlightable>().setHighlighted(true, 0.01f);

        if (Input.GetKeyUp(KeyCode.Mouse0) && hovered && hovered.GetComponent<OnGrid>()) {
            if (mFirstSquare.x == -1337.0f)
            {
                if (hovered)
                {
                    mSelectedSquare = hovered;
                    mFirstSquare = mSelectedSquare.GetComponent<OnGrid>().GetPosition();
                    mSelectedEntity = grid.GetGridContents(mFirstSquare);
                    if (mSelectedSquare.GetComponent<Highlightable>())
                        mSelectedSquare.GetComponent<Highlightable>().setHighlighted(true, float.MaxValue);
                }
            }
            else
            {
                if (mSelectedSquare.GetComponent<Highlightable>())
                    mSelectedSquare.GetComponent<Highlightable>().setHighlighted(false);
                var secondSquare = hovered.GetComponent<OnGrid>().GetPosition();
                grid.SwapGridContents(mFirstSquare, secondSquare);
                return true;
            }
        }

        return false;
    }

    public override void Reset()
    {
        mFirstSquare = new IntVec2(-1337, -1337);
        mSelectedEntity = null;
        mSelectedSquare = null;
    }
};
