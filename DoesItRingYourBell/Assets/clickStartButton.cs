using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseDown()
  {
    SendMessageUpwards("clickStart");
    StartCoroutine(prepareUser());
  }

  IEnumerator prepareUser(){
    Debug.Log("Ready?");
    yield return new WaitForSeconds(1);
    Debug.Log("Set!");
    yield return new WaitForSeconds(1);
    Debug.Log("Go!");
  }
}
