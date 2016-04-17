using UnityEngine;
using System.Collections;


public class ScuttlerBehavior : MonoBehaviour {

    private GameObject position;
    private Rigidbody2D rb;
    private GameObject player;
    private float initX;
    private float yTol = 1;
    private bool movingRight;
    private bool movingLeft;
    private bool lunging = false;

    public float patrolDist;
    public float speed;
    public float lungePower;
    public float lungeDist;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        movingRight = true;
        movingLeft = false;

        initX = rb.transform.position.x;


    }


    void Update() {


        float distance = player.transform.position.x - rb.transform.position.x;



        if (lunging == false)
        {

            if (distance < 0 && distance >= -lungeDist && OnPlane())
            {
                lunging = true;
                LungeLeft();
            }
            else if (distance > 0 && distance <= lungeDist && OnPlane())
            {
                lunging = true;
                LungeRight();
            }
            else
            {
                Patrol();
            }
        }


    }

    void LungeLeft()
    {
        rb.AddForce((Vector2.up + Vector2.left) * lungePower);
        StartCoroutine(explode());
    }

    void LungeRight()
    {
        rb.AddForce((Vector2.up + Vector2.right) * lungePower);
        StartCoroutine(explode());
    }

    void Patrol()
    {
        if (movingLeft)
        {
            if (transform.position.x - initX >= -patrolDist)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                movingLeft = false;
                movingRight = true;
            }
        }
        else
        {

            if (transform.position.x - initX <= patrolDist)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                movingRight = false;
                movingLeft = true;
            }
        }
    }

    void ReturnToPatrol()
    {
        if (rb.transform.position.x == initX)
        {
            lunging = false;
            return;
        }
    }


    bool OnPlane()
    {
        if (Mathf.Abs(rb.transform.position.y - player.transform.position.y) <= yTol)
            return true;
        else
            return false;
    }

    IEnumerator explode()
    {

        yield return new WaitForSeconds(0.75f);

        for (int i = 5; i>0; i--)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
    }

}
