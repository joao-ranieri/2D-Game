using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxScript : MonoBehaviour{

    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    public Transform groundCheck; //objeto responsavel por detectar se o personagem esta sobre uma superficie
    public Collider2D standing, crouching; //colisor em pe e agachado
    public GameObject fxDeath;

    public bool Grounded; //indidca se o personagem esta pisando no chao
    public bool lookLeft; //indica se o personagem esta virada para esquerda

    private int idAnimation; //indica o id da animacao

    private float horizontal, vertical;
    private float speed; //velocidade de movimento do personagem
    private float jumpForce; //forca aplicada para gerar o pulo do personagem
    
    // Start is called before the first frame update
    void Start(){
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    //possui taxa de atualizacao fixa de 0.02
    void FixedUpdate(){
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
    }

    // Update is called once per frame
    void Update() { 

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        speed = 0.8f;

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
        if ( Input.GetButtonDown("Jump") && Grounded == true){

            if (vertical < 0){
                jumpForce = 200;
            }
            else{
                jumpForce = 150;
            }

            playerRb.AddForce(new Vector2(0, jumpForce));
        }
        //fim do comando de pulo

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
            //StartCoroutine("deathAnimation"); 
        }
    }

    IEnumerator deathAnimation(){
        playerAnimator.SetTrigger("hit");
        yield return new WaitForSeconds(1);
        GameObject fxDie = Instantiate(fxDeath, playerRb.position, transform.localRotation);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1);
        Destroy(fxDie);
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
