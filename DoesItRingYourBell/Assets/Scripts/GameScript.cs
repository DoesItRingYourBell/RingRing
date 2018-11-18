using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  public Canvas canvas;
  private Text text;

  void Start () {
    tele = GetComponentsInChildren<telescript>();
    text = canvas.GetComponentInChildren<Text>();
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
      if(checkcounter != 0){
        tele[chain[checkcounter - 1]].clip.Stop();
        tele[chain[checkcounter - 1]].shaking = false;
        tele[chain[checkcounter - 1]].transform.localRotation = Quaternion.Euler(0,0,0);
      if(tele[chain[checkcounter - 1]] == test){
          tele[chain[checkcounter - 1]].activate();
        }
      }
      if(test == phoneToCheck){
        //StartCoroutine(blockUser());
        Debug.Log("Right Phone!");
        text.text = "Right Phone!";
        StartCoroutine(removeText());
        if(checkcounter == chain.Count - 1){
          checking = false;
          Debug.Log("Right Chain!");
          text.text = "Right Chain!";
          StartCoroutine(removeText());
          useractive = false;
          Appendchain();
        }else{
          checkcounter++;
          phoneToCheck = tele[chain[checkcounter]];
        }
      }else{
        Debug.Log("Wrong Phone!");
        text.text = "Wrong Phone!";
        StartCoroutine(removeText());
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
        text.text = "Ready?";
        yield return new WaitForSeconds(1);
        Debug.Log("Set!");
        text.text = "Set!";
        yield return new WaitForSeconds(1);
        Debug.Log("Go!");
        text.text = "Go!";
        StartCoroutine(removeText());
    }

    IEnumerator PlayMobil(List<int> phones){
    yield return new WaitForSeconds(3);
    foreach (var phone in phones) {
      tele[phone].activate();
      useractive = false;
      yield return new WaitForSeconds(3);
    }
    Debug.Log("Play Now");
    text.text = "Play Now";
    checkchain();
    StartCoroutine(removeText());
  }

  IEnumerator blockUser(){
    useractive = false;
    yield return new WaitForSeconds(3);
    useractive = true;
  }

  IEnumerator removeText(){
    yield return new WaitForSeconds(1);
    text.text = "";        
    var image = canvas.GetComponent<Image>();
    var tempColor = image.color;
    tempColor.a = 0f;
    image.color = tempColor;
  }

}
