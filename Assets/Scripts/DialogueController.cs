using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour{

    private GameObject dialogue;
    public TMP_Text entidade, caixaTexto;

    private string nomeEntidade;
    private string dialogo;

    private bool isDialogueOpen;

    private int countDialogue;

    // Start is called before the first frame update
    void Start(){
        dialogue = GameObject.Find("Dialogo");

        dialogue.SetActive(false);
        isDialogueOpen = false;

        countDialogue = 0;
        nomeEntidade = "Criador";
        entidade.SetText(nomeEntidade); // set entity name
        StartCoroutine(callDialog());
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Submit") && isDialogueOpen) {
            StartCoroutine(callDialog());
        }
    }

    IEnumerator callDialog(){
        if(countDialogue == 0){
            yield return new WaitForSeconds(1.5f);
            Time.timeScale = 0;
            dialogue.SetActive(true);
            isDialogueOpen = true;
            dialogo = "Muito bem ... Preparei este cenário mais simples para que você possa entender como as coisas funcionam ...";
            caixaTexto.SetText(dialogo);
        }
        if (countDialogue == 1){
            yield return new WaitForSeconds(0.5f);
            dialogo = "Primeiro você deve seguir a luz na escuridão, atravessar o que não poderia ser atravessado, e encontrar as escrituras que o guiarão.";
            caixaTexto.SetText(dialogo);
        }

        countDialogue++;
    }
}
