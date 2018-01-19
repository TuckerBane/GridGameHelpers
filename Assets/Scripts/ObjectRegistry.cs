using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRegistry : MonoBehaviour {

    [HideInInspector]
    public GameObject[] mUnits;

    public void AddToRegistry(GameObject unit)
    {
        Extensions.PushBack(ref mUnits, unit);
    }

    public void RemoveFromRegistry(GameObject unit)
    {
        Extensions.Erase(ref mUnits, unit);
    }
}
