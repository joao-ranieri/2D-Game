using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour{

    private GameObject firstPlan, secondPlan, codeImage, dialogue;
    public FadeScript fadeScript;
    public TMP_Text entidade, caixaTexto;

    private string nomeEntidade;
    private string dialogo;

    private int countDialogue;

    private bool isDialogEnd;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1; 

        codeImage = GameObject.Find("game_00");
        firstPlan = GameObject.Find("game_01");
        secondPlan = GameObject.Find("game_02");
        dialogue = GameObject.Find("Dialogo");

        isDialogEnd = false;
        countDialogue = 0;

        codeImage.SetActive(true); // start code image enabled
        firstPlan.SetActive(true); // start first background image enabled
        secondPlan.SetActive(false); // start second background image disabled
        dialogue.SetActive(false); // start doalogue box disabled

        // init first dialogue
        nomeEntidade = "Criador";
        entidade.SetText(nomeEntidade); // set entity name
        dialogo = "... Logo logo estará pronto para teste ... Só mais alguns ajustes ..."; // set dialogue text
        StartCoroutine(callDialog(dialogo, 1)); // call dialogue box
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Submit") && !isDialogEnd && countDialogue == 1){
            dialogo = "Ah... Você estava ai?... Não tinha percebido, já estava na hora. Estou finalizando algumas coisas para que você possa testar.";
            StartCoroutine(callDialog(dialogo, 2));
        } else if (Input.GetButtonDown("Submit") && isDialogEnd && countDialogue == 2){
            StartCoroutine(changeScene());
        }
    }

    IEnumerator callDialog(string dialogo, int image){
        if (image == 1){
            firstPlan.SetActive(true);
            secondPlan.SetActive(false);
        } else if (image == 2){
            fadeScript.animator.SetTrigger("fadeOut");
            yield return new WaitForSeconds(1);
            dialogue.SetActive(false);
            codeImage.SetActive(false);
            firstPlan.SetActive(false);
            secondPlan.SetActive(true);
            fadeScript.animator.SetTrigger("fadeIn");
            isDialogEnd = true;
        }
        yield return new WaitForSeconds(1);
        dialogue.SetActive(true);
        caixaTexto.SetText(dialogo);
        yield return new WaitForSeconds(2);
        countDialogue++;
    }

    IEnumerator changeScene(){
        fadeScript.animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(0.8f);
        dialogue.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SampleScene");
    }

}
