using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject);
	}
}
