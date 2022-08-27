using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/*Script responsável pelo comportamento do Menu Principal. Seu funcionamento é bem simples: exitem dois botões na cena "Menu",
um chamado "Novo Jogo" e outro "Sair", quando "Novo Jogo" é clicado, ele chama o método triggerMenuBehavior com o parâmetro 0,
fazendo com que o jogo prossiga para o primeiro level. O botão "Sair" chama o mesmo método, mas com parâmetro 1 fazendo com que
o jogo feche. */
public class MenuBehavior : MonoBehaviour
{

    public void triggerMenuBehavior(int i){
        switch(i){
            default:
            case(0):
                SceneManager.LoadScene("Level1");
                break;
            case(1):
                Application.Quit();
                break;
        }
    }
}
