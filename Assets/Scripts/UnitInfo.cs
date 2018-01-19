using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitInfo : ObjectRegistry
{
    public GameObject mNextInTurnOrder = null;

	// Update is called once per frame
	void Update () {
        mNextInTurnOrder = null;
        foreach(GameObject unit in mUnits)
        {
            if (!unit.GetComponent<PlayerSelectable>().mTurnTaken)
            {
                mNextInTurnOrder = unit;
                break;
            }
        }
	}

    public void RefreshUnits()
    {
        foreach (GameObject unit in mUnits)
        {
            unit.GetComponent<PlayerSelectable>().mTurnTaken = false;
        }
    }
}
