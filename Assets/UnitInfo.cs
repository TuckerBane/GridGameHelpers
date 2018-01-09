using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {

    List<UnitData> mUnits;

    class UnitData
    {
        GameObject mObject;
        bool mTurnTaken;
    }

	// Use this for initialization
	void Start () {
        mUnits = new List<UnitData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
