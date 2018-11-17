using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telescript : MonoBehaviour {
  AudioSource clip;
	// Use this for initialization
	void Start () {
		clip = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseDown()
  {
    activate();
  }

  void activate(){
    Debug.Log("Hit Object: " + gameObject.name);
    clip.Play();
    shake();
  }

  void shake(){
    
  }
}
