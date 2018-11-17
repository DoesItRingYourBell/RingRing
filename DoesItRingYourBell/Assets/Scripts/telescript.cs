using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telescript : MonoBehaviour {
  AudioSource clip;
	// Use this for initialization
  bool shaking = false;
  int shaking_count = 0;
  float stepping = 10.0f;
  bool forward = true;
  bool left = true;
  GameScript GameScript;
	void Start () {
		clip = this.GetComponent<AudioSource>();
    GameScript = transform.parent.GetComponent<GameScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(shaking){
      if(shaking_count%3 == 0 && shaking_count != 0){
        forward = !forward;
      }
      if(shaking_count%6 == 0 && shaking_count != 0){
        left = !left;
      }
      if(forward){
        stepping = 10.0f;
      }else{
        stepping = -10.0f;
      }
      var rot = transform.localRotation;
      float newRot = 0;
      if(left){
        newRot = rot.eulerAngles.z + stepping;
      }else{
        newRot = rot.eulerAngles.z - stepping;
      }
      transform.localRotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, newRot);
      shaking_count++;
      if(shaking_count == 24){
        shaking = false;
      }
    }
	}

  void OnMouseDown()
  {
    if(GameScript.useractive){
      activate();
      SendMessageUpwards("gotClicked", this);
    }
  }

  public void activate(){
    //Debug.Log("Hit Object: " + gameObject.name);
    clip.Play();
    shake();
  }

  void shake(){
    forward = true;
    shaking_count = 0;
    shaking = true;
  }
}
