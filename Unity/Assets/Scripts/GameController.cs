using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Método responsável pelo controle do jogo todo
public class GameController : MonoBehaviour
{
  
  public class Question{
    private int value1,value2;
    private char op;
    private int ident;

    public Question(int value1,int value2,char op, int ident)
    {
      this.value1 = value1;
      this.value2 = value2;
      this.op = op;
      this.ident = ident;
    }
    public int Value1
    {
      get{return value1;}
      set{value1 = value;}
    }
    public int Value2
    {
      get{return value2;}
      set{value2 = value;}
    }
    public char Op
    {
      get{return op;}
      set{op = value;}
    }
    public int Ident
    {
      get{return ident;}
      set{ident = value;}
    }
  };
  public class Answer{
    private int result;
    private int ident;

    public Answer(int result, int ident)
    {
      this.result = result;
      this.ident = ident;
    }
    public int Result
    {
      get{return result;}
      set{result = value;}
    }
    public int Ident
    {
      get{return ident;}
      set{ident = value;}
    }
  };

  public class CardPosition{
    private int identification;
    private int type;

    public CardPosition(int identification,int type)
    {
      this.identification = identification;
      this.type = type;
    }
    public int Identification
    {
      get{return identification;}
      set{identification = value;}
    }
    public int Type
    {
      get{return type;}
      set{type = value;}
    }
  }

  [SerializeField]
  private Sprite bgImage;
  [SerializeField]
  private Sprite frontImage;
  [SerializeField]
  private TextMeshProUGUI pairWarning;
  [SerializeField]
  private Button nextLevelButton;

  //Listas
  public List<Question> qList = new List<Question>();
  public List<Answer> aList = new List<Answer>();
  public List<Button> btns = new List<Button>();
  public List<string> cardTextQ = new List<string>();
  public List<string> cardTextA = new List<string>();
  public List<CardPosition> positionList = new List<CardPosition>();

  //Variaveis
  private bool firstSelected, secondSelected;
  private int firstSelectedPosition,secondSelectedPosition;
  private int firstSelectedIdentificator, secondSelectedIdentificator;
  private int totalPairs,pairCount = 0 ;

  void Start(){
    disableObjects();
    GetButtons();
    CreateCards();
    OpToString();
    SetPosition();
    textSetup();
    AddListeners();
    totalPairs = btns.Count/2;

  }

void disableObjects()
{
  pairWarning.enabled = false;

}
void GetButtons(){
  GameObject [] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
  for(int i=0;i<objects.Length;i++)
  {
    btns.Add(objects[i].GetComponent<Button>());
    btns[i].image.sprite = bgImage;
    btns[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
  }
}

void textSetup()
{
  for(int i=0;i<16;i++)
  {
    if(positionList[i].Type == 1)
    {
      btns[i].GetComponentInChildren<TextMeshProUGUI>().text = cardTextQ[positionList[i].Identification];
    }
    else
    {
      btns[i].GetComponentInChildren<TextMeshProUGUI>().text = cardTextA[positionList[i].Identification];
    }
  }
}

void AddListeners(){
  foreach(Button btn in btns)
  {
    btn.onClick.AddListener(()=>PickAPuzzle());
  }
}
public void PickAPuzzle(){

  string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
  if(!firstSelected)
  {
    firstSelected = true;
    firstSelectedPosition = int.Parse(name);
    btns[firstSelectedPosition].interactable = false;
    firstSelectedIdentificator = positionList[firstSelectedPosition].Identification;
    btns[firstSelectedPosition].image.sprite = frontImage;
    btns[firstSelectedPosition].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
  }
  else if(!secondSelected)
  {
    secondSelected = true;
    secondSelectedPosition = int.Parse(name);
    btns[secondSelectedPosition].interactable = false;
    secondSelectedIdentificator = positionList[secondSelectedPosition].Identification;
    btns[secondSelectedPosition].image.sprite = frontImage;
    btns[secondSelectedPosition].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    StartCoroutine(CheckMatch());

  }

}


IEnumerator CheckMatch(){

    yield return new WaitForSeconds (.25f);

    if(firstSelectedIdentificator == secondSelectedIdentificator)
    {
      yield return new WaitForSeconds(.1f);
      pairWarning.text = "Formou Par!";
      pairWarning.enabled = true;
      CheckGameFinish();
      yield return new WaitForSeconds(1f);
      pairWarning.enabled = false;
    }
    else
    {
      yield return new WaitForSeconds(.1f);
      pairWarning.text = " Nao Formou Par!";
      pairWarning.enabled = true;
      btns[firstSelectedPosition].image.sprite = bgImage;
      btns[firstSelectedPosition].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
      btns[firstSelectedPosition].interactable = true;
      btns[secondSelectedPosition].image.sprite = bgImage;
      btns[secondSelectedPosition].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
      btns[secondSelectedPosition].interactable = true;
      yield return new WaitForSeconds(1f);
      pairWarning.enabled = false;
    }
      firstSelected = false;
      secondSelected = false;
}

void CheckGameFinish(){
  pairCount++;

  if(pairCount == totalPairs)
  {
    //level completo
    pairWarning.enabled = true;
    pairWarning.text = " Voce Venceu!";
    //SceneManager.LoadScene("Menu");
  }
}

void OpToString(){

  string val1,val2,ans;
  for(int i =0;i<8;i++)
  {
    val1 = qList[i].Value1.ToString();
    val2 = qList[i].Value2.ToString();
    ans = aList[i].Result.ToString();
    cardTextQ.Add(val1+qList[i].Op+val2);
    cardTextA.Add(ans);
    //Debug.LogFormat("{0},{1}",cardTextQ[i],cardTextA[i]);
  }

}
void CreateCards()
{
  int num1,num2,operation,ans;
  char opconv;
  for(int i=0;i<8;i++)
  {
    operation = Random.Range(1,5);
    if(operation == 1)
    {
      num1 = Random.Range(1,99);
      num2 = Random.Range(1,99);
      opconv = '+';
      ans = num1 + num2;
    }
    else if(operation == 2)
    {
      num1 = Random.Range(1,99);
      num2 = Random.Range(1,99);
      opconv = '-';
      ans = num1 - num2;
    }
    else if(operation == 3)
    {
      num1 = Random.Range(1,20);
      num2 = Random.Range(1,20);
      opconv = 'X';
      ans = num1 * num2;
    }
    else
    {
      num1 = Random.Range(1,99);
      num2 = Random.Range(1,99);
      while(num1%num2!=0)
      {
        num1 = Random.Range(1,99);
        num2 = Random.Range(1,99);
      }
      opconv = '/';
      ans = num1/num2;
    }

    qList.Add(new Question(num1,num2,opconv,i));
    aList.Add(new Answer(ans,i));

  }
/*  for(int i=0;i<8;i++)
    {
      Debug.LogFormat("{0},{1},{2},{3},{4},{5}",qList[i].Value1,qList[i].Op,qList[i].Value2,qList[i].Ident,aList[i].Result,aList[i].Ident);
    }*/
}

void SetPosition()
{
  int pos,type,cont=0;
  bool checkEqual = false;
  while(cont<16)
  {
    pos = Random.Range(0,8);
    type = Random.Range(1,3);
    positionList.Add(new CardPosition(pos,type));
    for(int i=0;i<cont&&!checkEqual;i++)
    {
      if(positionList[i].Identification == positionList[cont].Identification && positionList[i].Type == positionList[cont].Type)
      {
        checkEqual = true;
      }
    }
    if(checkEqual)
    {
      positionList.RemoveAt(cont);
      checkEqual = false;
    }
    else
    {
      cont++;
    }
  }
  /*foreach(var position in positionList)
  {
    Debug.LogFormat("{0},{1}",position.Identification,position.Type);
  }*/
}

public void returnToMenu(bool i)
{
  if(i)
  {
    SceneManager.LoadScene("Menu");
  }
}
}//gamecontroller
