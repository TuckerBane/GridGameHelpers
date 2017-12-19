using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turn
{
    public Turn(string name) {
        Reset();
        mName = name;
    }
    public virtual bool Update() { return true; }

    public virtual void Reset() { }

    public bool mSkipped = false;
    public string mName = "unnamed turn";
}

public class TurnSystem : MonoBehaviour {


    public List<Turn> mTurnOrder;
    public int currentTurn = 0;

	// Use this for initialization
	void Start () {
        mTurnOrder = new List<Turn>();
        mTurnOrder.Add(new PlayerTurn());
        Turn cleanup = new Turn("between turn cleanup");
        mTurnOrder.Add(cleanup);
        mTurnOrder.Add(new Turn("enemy turn"));
        mTurnOrder.Add(cleanup);

    }
	
    void Testing(Color c)
    {
        c.a = 5;
    }

	// Update is called once per frame
	void Update () {



        if (mTurnOrder[currentTurn].Update() || Input.GetKeyDown("space"))
        {
            mTurnOrder[currentTurn].Reset();
            currentTurn = (currentTurn + 1) % mTurnOrder.Count;
            Debug.Log("current turn is " + mTurnOrder[currentTurn].mName);
        }

	}
}
