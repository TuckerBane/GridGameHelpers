using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// should probably be an interface
public class Turn
{
    public Turn(string name) {
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
    public UnitInfo mPlayer1Units;
    public UnitInfo mPlayer2Units;

    // Use this for initialization
    void Start () {
        mTurnOrder = new List<Turn>();
        mTurnOrder.Add(new Turn("round start"));
        mTurnOrder.Add(new PlayerTurn(mPlayer1Units, PlayerID.PlayerOne));
        Turn cleanup = new Turn("between turn cleanup");
        mTurnOrder.Add(cleanup);
        mTurnOrder.Add(new Turn("enemy turn"));
        mTurnOrder.Add(new PlayerTurn(mPlayer2Units, PlayerID.PlayerTwo));
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
