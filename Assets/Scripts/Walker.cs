using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public float time = 0.5f;
    public float stepTime = 1.0f;
    public GridLogic grid;
    public int x = 0;
    public int y = 0;
	// Use this for initialization
	void Start () {
        grid = FindObjectOfType<GridLogic>();
        
    }
	
	// Update is called once per frame
	void Update () {

        
        time -= Time.deltaTime;
		if(time <= 0)
        {
            ++x;
            grid.PlaceOnGrid(this.gameObject, new Vector2(x, y ));
            time += stepTime;
        }
        
    }
}
