using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Método responsável por instanciar todas as cartas do jogo como botões, além de nomea-las
como números de 0 a 16, tornando-se mais fácil fazer referências a elas dentro dos métodos.*/

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform CardList;

    [SerializeField]
    private GameObject btn;

    void Awake(){

      for(int i=0;i<16;i++)
      {
          GameObject button = Instantiate(btn);
          button.name = ""+i;
          button.transform.SetParent(CardList,false);
      }
    }
}
