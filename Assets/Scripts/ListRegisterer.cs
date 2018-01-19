using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class ListRegisterer : MonoBehaviour {

    // pick one of these and go through the meta data to find a list to add yourself to
    public ObjectRegistry mObjectRegistrationTarget;

    // Use this for initialization
    void OnEnable ()
    {
        mObjectRegistrationTarget.AddToRegistry(gameObject);
	}

    private void OnDisable()
    {
        mObjectRegistrationTarget.RemoveFromRegistry(gameObject);
    }
}
