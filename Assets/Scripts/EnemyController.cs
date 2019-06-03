using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{

    private Animator enemyAnimator;

    protected Rigidbody2D enemyRB;
    protected Animator animator;
    protected Transform player;
    protected SpriteRenderer spriteRenderer;

    protected bool isAlive = true;
    protected bool isMoving = false;

    public GameObject fxDeath;
    public LayerMask layerObstacles;
    public Vector3 dir = Vector3.right;

    public float speed;
    public float distanceAtack;
    public float distanceChangeRoute;

    void Awake(){
        speed = 0.4f;
        distanceChangeRoute = 0.2f;

        enemyAnimator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Fox").GetComponent<Transform>();
    }

    protected float playerDistance() {
        return Vector2.Distance(player.position, transform.position);
    }

    protected void flip(){
        spriteRenderer.flipX = !spriteRenderer.flipX;
        speed *= -1;
    }

    protected void patrol(){
        Debug.DrawRay(transform.position, dir * -1 * distanceChangeRoute, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir * -1, distanceChangeRoute, layerObstacles);

        if (hit) {
            flip();
            dir *= -1;
        }

        enemyRB.velocity = new Vector2(speed, enemyRB.velocity.y);
    }

    IEnumerator deathAnimation(){
        Destroy(this.gameObject);
        GameObject fxDie = Instantiate(fxDeath, enemyRB.position, transform.localRotation);
        yield return new WaitForSeconds(0.7f);
        Destroy(fxDie);
    }

}
