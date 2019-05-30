using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumScript : EnemyController { 

    // Start is called before the first frame update
    void Start() {
        speed = 0.4f;
        distanceAtack = 0.5f;
        distanceChangeRoute = 0.25f;

        float x = transform.localScale.x * (-1); //invert o valor do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

    }

    // Update is called once per frame
    void Update(){

        float distance = playerDistance();
        isMoving = (distance <= distanceAtack);

        if (isMoving){
            if ( (player.position.x > transform.position.x && spriteRenderer.flipX) || 
                    (player.position.x < transform.position.x && !spriteRenderer.flipX) ) {
                flip();
            }
        } else {
            patrol();
        }
    }

    //possui taxa de atualizacao fixa de 0.02
    void FixedUpdate(){
        if (isMoving){
            enemyRB.velocity = new Vector2(speed, enemyRB.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Fox" && collision.GetType().Name == "CircleCollider2D"){
            StartCoroutine("deathAnimation");
        }
    }
}
