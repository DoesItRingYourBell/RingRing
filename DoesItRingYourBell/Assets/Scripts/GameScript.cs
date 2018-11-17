﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
  private telescript[] tele;
  private List<int>chain;
  private bool once = true;
  private telescript phoneToCheck;
  private bool checking = false;
  private int checkcounter = 0;
  private bool flaweless = false;
  // Use this for initialization
  void Start () {
    tele = GetComponentsInChildren<telescript>();
  }
  
  // Update is called once per frame
  void Update () {
    if(once){
      once = false;
      StartGame();
    }
  }

  void StartGame(){
    chain = new List<int>();
    Appendchain();
  }

  void checkchain(){
    phoneToCheck = tele[chain[0]];
    checking = true;
    checkcounter = 0;
    flaweless = true;
    //Debug.Log(chain);
  }

  void Appendchain(){
    var Random = new System.Random();
    int telephone = Random.Next(0, tele.Length);
    chain.Add(telephone);
    StartCoroutine(PlayMobil(chain));
  }

  void gotClicked(telescript test){
    if(checking){
      //Debug.Log(checkcounter);
      if(test == phoneToCheck){
        Debug.Log("Right Phone!");
        if(checkcounter == chain.Count - 1){
          checking = false;
          Debug.Log("Right Chain!");
          Appendchain();
        }else{
          checkcounter++;
          phoneToCheck = tele[chain[checkcounter]];
        }
      }else{
        Debug.Log("Wrong Phone!");
        checking = false;
      }
      
      /*if(checkcounter == chain.Count - 1){
        checking = false;
      }else{
        checkcounter++;
      }*/
    }
  }

  IEnumerator PlayMobil(List<int> phones){
    yield return new WaitForSeconds(3);
    foreach (var phone in phones) {
      tele[phone].activate();
      yield return new WaitForSeconds(3);
    }
    Debug.Log("Play Now");
    checkchain();
  }
}
