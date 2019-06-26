using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour{

    public DialogueController dialogueController;

    public GameObject leaverUp, leaverDown, bookCase, deathBox, door;
    private bool findBookCase, findLeaver;

    // Start is called before the first frame update
    void Start(){
        leaverDown.SetActive(false);

        findBookCase = false;
    }

    // Update is called once per frame
    void Update(){
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision.gameObject.name);
    }

    public void disableBookCase(){
        findBookCase = true;
        bookCase.SetActive(false);
        dialogueController.countDialogue++;
        dialogueController.StartCoroutine("callDialog");
    }

    public void disableLeaver(){
        findLeaver = true;
        leaverUp.SetActive(false);
        leaverDown.SetActive(true);
        deathBox.SetActive(false);
        dialogueController.countDialogue++;
        dialogueController.StartCoroutine("callDialog");
    }

    public void disableDoor(){
        dialogueController.countDialogue++;
        dialogueController.StartCoroutine("callDialog");
    }
}
