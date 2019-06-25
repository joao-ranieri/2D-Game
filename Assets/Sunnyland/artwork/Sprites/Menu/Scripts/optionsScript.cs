using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsScript : MonoBehaviour{

    public FadeScript fadeScript;
    private SpriteRenderer srPressEnter;
    private GameObject goTitle, goInstructions;
    public Color[] objectColor;

    private bool titleActive, instructionsActive;

    private void Awake(){
    }

    // Start is called before the first frame update
    void Start(){
        srPressEnter = GameObject.Find("pressEnter").GetComponent<SpriteRenderer>();
        goTitle = GameObject.Find("title");
        goInstructions = GameObject.Find("instructions");

        titleActive = true;
        instructionsActive = false;

        goInstructions.SetActive(false);
        srPressEnter.color = objectColor[0];
        StartCoroutine("pressEnter");
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Submit") && titleActive) {
            titleActive = false;
            instructionsActive = true;
            goTitle.SetActive(false);
            goInstructions.SetActive(true);

        } else if (Input.GetButtonDown("Submit") && instructionsActive) {
            instructionsActive = false;
            //goInstructions.SetActive(false);
        } else if ( Input.GetButtonDown("Submit") && !titleActive && !instructionsActive){
            StartCoroutine("changeScene");
        }
    }

    IEnumerator changeScene(){
        fadeScript.FadeToLevel(1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Intro");
    }

    IEnumerator pressEnter(){
        do{
            srPressEnter.color = objectColor[0];
            yield return new WaitForSeconds(0.4f);
            srPressEnter.color = objectColor[1];
            yield return new WaitForSeconds(0.4f);
        } while (titleActive == true);
    }
}
