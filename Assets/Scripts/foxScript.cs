using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class foxScript : MonoBehaviour{

    public PauseScript pauseScript;
    public ControllerScript controllerScript;

    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    public Transform groundCheck; //objeto responsavel por detectar se o personagem esta sobre uma superficie
    public Collider2D standing, crouching; //colisor em pe e agachado
    public GameObject fxDeath;
    public GameObject alertBaloon;

    public bool Grounded; //indidca se o personagem esta pisando no chao
    public bool lookLeft; //indica se o personagem esta virada para esquerda
    private bool canMove;
    private bool isInteracting;
    private bool findBookcase;
    private bool findLeaver;

    private string objectInteracting;

    private int idAnimation; //indica o id da animacao

    private float horizontal, vertical;
    private float speed; //velocidade de movimento do personagem
    private float jumpForce; //forca aplicada para gerar o pulo do personagem
    
    // Start is called before the first frame update
    void Start(){
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        canMove = true;
        isInteracting = false;
        alertBaloon.SetActive(false);
    }

    //possui taxa de atualizacao fixa de 0.02
    void FixedUpdate(){
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
    }

    // Update is called once per frame
    void Update() {

        if (canMove) {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            speed = 0.8f;
        }

        //comeco da verificacao da direcao do personagem
        if (horizontal > 0 && lookLeft == true) {
            flip();
        }
        else if (horizontal < 0 && lookLeft == false){
            flip();
        }
        //fim da verificacao da direcao do personagem

        //comeco da verificacao da direcao e movimentacao
        if (vertical < 0){
            idAnimation = 2;
            if (Grounded == true) {
                horizontal = 0;
            }
        }
        else if (horizontal != 0){
            idAnimation = 1;
        }
        else {
            idAnimation = 0;
        }
        //fim da verificacao da direcao e movimentacao

        //comeco do comando de pulo
        if ( Input.GetButtonDown("Jump") && Grounded == true && canMove == true && !isInteracting){

            if (vertical < 0){
                jumpForce = 200;
            }
            else{
                jumpForce = 150;
            }

            playerRb.AddForce(new Vector2(0, jumpForce));
        }
        //fim do comando de pulo


        if (isInteracting && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))){
            if (objectInteracting == "bookcase") {
                findBookcase = true;
                controllerScript.disableBookCase();
                endInteraction();
            }
            else if (objectInteracting == "leaver") {
                findLeaver = true;
                controllerScript.disableLeaver();
                endInteraction();
            }
            else if (objectInteracting == "door"){
                controllerScript.disableDoor();
                endInteraction();
            }
        }

        //
        if (vertical < 0 && Grounded == true) {
            crouching.enabled = true;
            standing.enabled = false;
        }
        else {
            crouching.enabled = false;
            standing.enabled = true;
        }

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
    }

    //funcao para virar o personagem
    void flip(){
        lookLeft = !lookLeft; //invert o valor da variavel boleana
        float x = transform.localScale.x;
        x *= -1; //invert o valor do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Opossum" && collision.GetType().Name == "CapsuleCollider2D") {
            collision.gameObject.SendMessage("changeCanFallow");
            StartCoroutine("deathAnimation");
        }
        if (collision.gameObject.name == "spikes" && collision.GetType().Name == "EdgeCollider2D"){
            StartCoroutine("deathAnimation");
        }
        if (collision.gameObject.name == "DeathBox" && collision.GetType().Name == "BoxCollider2D"){
            StartCoroutine("deathAnimation");
        }
        if (collision.gameObject.name == "Bookcase"){
            callInteraction("bookcase");
        }
        if (collision.gameObject.name == "Leaver" && findBookcase){
            callInteraction("leaver");
        }
        if (collision.gameObject.name == "Door" && findLeaver){
            callInteraction("door");
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        endInteraction();
    }

    void callInteraction(string interaction){
        objectInteracting = interaction;
        isInteracting = true;
        alertBaloon.SetActive(true);
    }

    void endInteraction(){
        objectInteracting = "";
        isInteracting = false;
        alertBaloon.SetActive(false);
    }

    IEnumerator deathAnimation(){
        canMove = false;
        this.gameObject.layer = 13; // layer 13 representa a layer de inimigos, ela nao colide com outros inimigos

        if (lookLeft){
            playerRb.AddForce(new Vector2(250, 0));
        } else {
            playerRb.AddForce(new Vector2(-250, 0));
        }

        yield return new WaitForSeconds(0.2f);
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        playerAnimator.SetTrigger("hit");
        yield return new WaitForSeconds(0.5f);
        GameObject fxDie = Instantiate(fxDeath, playerRb.position, transform.localRotation);
        //Destroy(this.gameObject);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        pauseScript.callGameOver();
    }

    void getBookCase(){

    }
}

/*
Animation list
0 - idle
1 - run
2 - crouch
3 - hurt
4 - die
*/
