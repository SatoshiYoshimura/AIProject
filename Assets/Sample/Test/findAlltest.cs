using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findAlltest : MonoBehaviour {

    List<int> intList = null;
	// Use this for initialization
	void Start () {
        intList = new List<int>();
        for (int i = 0; i < 100; i++) {
            intList.Add(i);
        }

        List<int> tmpList = intList.FindAll( i => i > 50);
        foreach (var val in tmpList) {
            Debug.Log(val);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
