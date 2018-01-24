using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeOnGrid : MonoBehaviour {

    private IntVec2 mPlaceToGo;

    private void Start()
    {
        mPlaceToGo = transform.position;
    }

    // Update is called once per frame
    void Update () {
        FindObjectOfType<GridLogic>().PlaceOnGrid(this.gameObject, mPlaceToGo);
        Destroy(this);
	}
}
