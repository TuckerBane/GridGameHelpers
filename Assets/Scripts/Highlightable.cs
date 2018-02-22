using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour {

    public Material mNormalMaterial;
    public Material mHighlightedMateril;
    public float mTimeOfLastStatusChange = 0;

    public bool mHighlighted = false;
    public float mHighlightEndTime = 0;

    public void SetHighlighted(bool highlight, float duration = 0.2f)
    {
        if (highlight != mHighlighted)
            mTimeOfLastStatusChange = Time.time;

        if (highlight)
        {
            GetComponent<MeshRenderer>().material = mHighlightedMateril;
            mHighlightEndTime = Mathf.Max(Time.time + duration, mHighlightEndTime);
        }
        else
        {
            GetComponent<MeshRenderer>().material = mNormalMaterial;
            mHighlightEndTime = 0;
        }
        mHighlighted = highlight;
    }

    public void Update()
    {
        if(mHighlighted && Time.time >= mHighlightEndTime)
        {
            SetHighlighted(false);
        }
    }

}
