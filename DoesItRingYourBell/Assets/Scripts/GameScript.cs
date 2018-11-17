using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
  private telescript[] tele;
	// Use this for initialization
	void Start () {
		tele = GetComponentsInChildren<telescript>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
