using System.Collections;
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
  public bool useractive = false;
  private bool gameActive = false;
  // Use this for initialization
  void Start () {
    tele = GetComponentsInChildren<telescript>();
  }
  
  // Update is called once per frame
  void Update () {
  }

  void StartGame(){
    if(!gameActive){
      gameActive = true;
      StartCoroutine(prepareUser());
      for(int i = 0; i < tele.Length; i++){
        tele[i].shaking = false;
        tele[i].clip.Stop();
        tele[i].transform.localRotation = Quaternion.Euler(0,0,0);
      }
      chain = new List<int>();
      Appendchain();
    }
  }

  void checkchain(){
    phoneToCheck = tele[chain[0]];
    checking = true;
    checkcounter = 0;
    flaweless = true;
    useractive = true;
  }

  void Appendchain(){
    var Random = new System.Random();
    int telephone = Random.Next(0, tele.Length);
    chain.Add(telephone);
    StartCoroutine(PlayMobil(chain));
  }

  void gotClicked(telescript test){
    if(checking){
      if(test == phoneToCheck){
        //StartCoroutine(blockUser());
        if(checkcounter != 0 && tele[chain[checkcounter - 1]] != tele[chain[checkcounter]]){
          tele[chain[checkcounter - 1]].clip.Stop();
          tele[chain[checkcounter - 1]].shaking = false;
          tele[chain[checkcounter - 1]].transform.localRotation = Quaternion.Euler(0,0,0);
        }
        Debug.Log("Right Phone!");
        if(checkcounter == chain.Count - 1){
          checking = false;
          Debug.Log("Right Chain!");
          useractive = false;
          Appendchain();
        }else{
          checkcounter++;
          phoneToCheck = tele[chain[checkcounter]];
        }
      }else{
        Debug.Log("Wrong Phone!");
        checking = false;
        useractive = false;
        gameActive = false;
      }
    }
  }

  void clickStart(){
     StartGame();
  }

    IEnumerator prepareUser()
    {
        Debug.Log("Ready?");
        yield return new WaitForSeconds(1);
        Debug.Log("Set!");
        yield return new WaitForSeconds(1);
        Debug.Log("Go!");
    }

    IEnumerator PlayMobil(List<int> phones){
    yield return new WaitForSeconds(3);
    foreach (var phone in phones) {
      tele[phone].activate();
      useractive = false;
      yield return new WaitForSeconds(3);
    }
    Debug.Log("Play Now");
    checkchain();
  }

  IEnumerator blockUser(){
    useractive = false;
    yield return new WaitForSeconds(3);
    useractive = true;
  }
}
