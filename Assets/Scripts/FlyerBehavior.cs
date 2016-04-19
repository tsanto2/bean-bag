

using UnityEngine;
using System.Collections;

public class FlyerBehavior : MonoBehaviour
{


    GameObject flyer;
    private PlayerMove player;
    float range;
    public int health;
    public LayerMask mask;
    public float speed;
    public float targetDistance;
    public Transform target;


public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.health -= 10;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Invincible")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }

        if (col.tag == "Drill")
        {
            Destroy(this.gameObject);
        }

        if (col.tag == "Explosion")
        {
            Destroy(this.gameObject);
        }
    }



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {

        

        float playerDistance = Vector2.Distance(player.transform.position, this.transform.position);

        print(playerDistance.ToString());

        if (SeesPlayer() && (Mathf.Abs(playerDistance) <= targetDistance))
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);


    }

    bool SeesPlayer()
    {
        print("Boob");
        if (Physics2D.Linecast(transform.position, player.transform.position, mask))
            return false;
        else
            return true;
    }
}


