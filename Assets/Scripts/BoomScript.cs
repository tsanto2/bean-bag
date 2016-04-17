using UnityEngine;
using System.Collections;

public class BoomScript : MonoBehaviour {

    public int bounceCount;
    public bool facingRight;
    public bool launched;

    private PlayerMove player;
    private Rigidbody2D rb;
    private BoomSpawn boomSpawn;

    // Use this for initialization
    void Start()
    {
        bounceCount = 0;
        player = GameObject.FindObjectOfType<PlayerMove>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        boomSpawn = GameObject.FindObjectOfType<BoomSpawn>();
        facingRight = player.facingRight;
        if (facingRight)
        {
            rb.AddForce((Vector2.up * 625.0f) + (Vector2.right * 350.0f));
        }
        else {
            rb.AddForce((Vector2.up * 625.0f) + (Vector2.left * 350.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RT") > 0 && facingRight && bounceCount == 0 && !launched && !boomSpawn.rDown)
        {
            launched = true;
            Vector3 v = rb.velocity;
            v.x = 0;
            v.y = 0;
            rb.velocity = v;
            rb.AddForce((Vector2.up * 625.0f) + (Vector2.left * 350.0f));
            bounceCount++;
        }
        if (Input.GetAxis("RT") > 0 && !facingRight && bounceCount == 0 && !launched && !boomSpawn.rDown)
        {
            launched = true;
            Vector3 v = rb.velocity;
            v.x = 0;
            v.y = 0;
            rb.velocity = v;
            rb.AddForce((Vector2.up * 625.0f) + (Vector2.right * 350.0f));
            bounceCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag != "Player")
        {
            Instantiate(boomSpawn.boom, transform.position, transform.rotation);
            Destroy(gameObject);
            boomSpawn.isActive = false;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        boomSpawn.isActive = false;
    }
}
