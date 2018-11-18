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
  public Canvas playgroundCanvas;
  private Text playgroundText;
  public Text counterText;
  public Button newGameButton;

    void Start () {
    tele = GetComponentsInChildren<telescript>();
    playgroundText = playgroundCanvas.GetComponentInChildren<Text>();
  }

  void StartGame(){
    if(!gameActive){
      counterText.text = "0";
      gameActive = true;
      newGameButton.interactable = false;
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
        if(checkcounter == chain.Count - 1){
          counterText.text = (checkcounter + 1).ToString();
          checking = false;
          useractive = false;
          Appendchain();
        }else{
          checkcounter++;
          phoneToCheck = tele[chain[checkcounter]];
        }
      }else{
        Debug.Log("Wrong Phone!");
        var image = playgroundCanvas.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.8f;
        image.color = tempColor;
        playgroundText.text = "Spiel verloren!";
        //StartCoroutine(removeText());
        checking = false;
        useractive = false;
        gameActive = false;
        newGameButton.interactable = true;
      }
    }
  }

  void clickStart(){
     StartGame();
  }

    IEnumerator prepareUser()
    {
        Debug.Log("Ready?");
        playgroundText.text = "Auf die Plätze.";
        yield return new WaitForSeconds(1);
        Debug.Log("Set!");
        playgroundText.text = "Fertig?";
        yield return new WaitForSeconds(1);
        Debug.Log("Go!");
        playgroundText.text = "Los!";
        StartCoroutine(removeText());
    }

    IEnumerator PlayMobil(List<int> phones){
    yield return new WaitForSeconds(3);
    foreach (var phone in phones) {
      tele[phone].activate();
      useractive = false;
      yield return new WaitForSeconds(3);
    }
    checkchain();
  }

  IEnumerator blockUser(){
    useractive = false;
    yield return new WaitForSeconds(3);
    useractive = true;
  }

  IEnumerator removeText(){
    yield return new WaitForSeconds(1);
    playgroundText.text = "";        
    var image = playgroundCanvas.GetComponent<Image>();
    var tempColor = image.color;
    tempColor.a = 0f;
    image.color = tempColor;
  }

}
