using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour{

    private GameObject dialogue;
    public TMP_Text entidade, caixaTexto;

    private string nomeEntidade;
    private string dialogo;

    private bool isDialogueOpen;

    public int countDialogue;

    // Start is called before the first frame update
    void Start(){
        dialogue = GameObject.Find("Dialogo");

        dialogue.SetActive(false);
        isDialogueOpen = true;

        countDialogue = 0;
        nomeEntidade = "Criador";
        entidade.SetText(nomeEntidade); // set entity name
        StartCoroutine(callDialog());
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Submit") && isDialogueOpen){
            StopAllCoroutines();
            dialogue.SetActive(false);
            StartCoroutine(callDialog());
        }
        else if (Input.GetButtonDown("Submit") && !isDialogueOpen){
            Time.timeScale = 1;
            dialogue.SetActive(false);
        }
    }

    IEnumerator callDialog(){
        if(countDialogue == 0){
            yield return new WaitForSeconds(0.5f);
            Time.timeScale = 0;
            dialogue.SetActive(true);
            dialogo = "Muito bem ... Preparei este cenário mais simples para que você possa entender como as coisas funcionam ...";
            caixaTexto.SetText(dialogo);
            countDialogue++;
        }
        else if (countDialogue == 1){
            isDialogueOpen = false;
            dialogue.SetActive(true);
            dialogo = "Primeiro você deve seguir a luz na escuridão, atravessar o que não poderia ser atravessado, e encontrar as escrituras que o guiarão.";
            caixaTexto.SetText(dialogo);
        }
        else if (countDialogue == 2){
            yield return new WaitForSeconds(0.2f);
            isDialogueOpen = true;
            Time.timeScale = 0;
            dialogue.SetActive(true);
            dialogo = "Ótimo, você já conseguiu encontrar um dos objetivos ... O próximo objetivo não será difícil.";
            caixaTexto.SetText(dialogo);
            countDialogue++;
        }
        else if (countDialogue == 3){
            isDialogueOpen = false;
            dialogue.SetActive(true);
            dialogo = "Busque agora o túmulo daqueles que vieram antes de você, a história dos que já se foram, pode abrir o seu caminho.";
            caixaTexto.SetText(dialogo);
        }
        else if (countDialogue == 4){
            yield return new WaitForSeconds(0.2f);
            isDialogueOpen = true;
            Time.timeScale = 0;
            dialogue.SetActive(true);
            dialogo = "Já era de se esperar, este nível não é tão difícil. Vamos lá! Leia bem a próxima dica ...";
            caixaTexto.SetText(dialogo);
            countDialogue++;
        }
        else if (countDialogue == 5){
            isDialogueOpen = false;
            dialogue.SetActive(true);
            dialogo = "Agora o caminho através da morte está aberto, aquilo que antes lhe matava agora não matará. Corra, pule, caia, encontre a porta. O próximo nível o espera!";
            caixaTexto.SetText(dialogo);
        }
        else if (countDialogue == 6){
            yield return new WaitForSeconds(0.2f);
            isDialogueOpen = true;
            Time.timeScale = 0;
            dialogue.SetActive(true);
            dialogo = "Interessante ... Acho que preciso dificultar mais este cenário. Vamos para o próximo ...";
            caixaTexto.SetText(dialogo);
            countDialogue++;
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
