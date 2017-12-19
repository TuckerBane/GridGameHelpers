using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeOnGrid : MonoBehaviour {

    public IntVec2 mPlaceToGo;
	
	// Update is called once per frame
	void Update () {
        FindObjectOfType<GridLogic>().PlaceOnGrid(this.gameObject, mPlaceToGo);
        Destroy(this);
	}
}
