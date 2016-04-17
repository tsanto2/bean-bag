using UnityEngine;
using System.Collections;

public class ShortStuff : MonoBehaviour {

    public int health;
    public bool hit;

    private PlayerMove player;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMove>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = 5;
        StartCoroutine(WalkLeft());
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            health -= col.GetComponent<Bullet>().damage;
        }
        if (col.tag == "Explosion")
        {
            health -= 5;
        }
        if (col.tag == "Drill")
        {
            hit = true;
            StartCoroutine(HitWait());

            if (player.facingRight)
            {
                rb.AddForce((Vector2.right + Vector2.up) * 150.0f);
                health -= 3;
            }
            if (!player.facingRight)
            {
                rb.AddForce((Vector2.left + Vector2.up) * 150.0f);
                health -= 3;
            }
        }
    }

    IEnumerator WalkLeft()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0);
            if (!hit)
                transform.Translate(Vector3.left * Time.deltaTime * 5.0f);
        }

        StartCoroutine(WalkRight());
    }

    IEnumerator WalkRight()
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0);
            if (!hit)
                transform.Translate(Vector3.right * Time.deltaTime * 5.0f);
        }

        StartCoroutine(WalkLeft());

    }

    IEnumerator HitWait()
    {
        yield return new WaitForSeconds(0.75f);
        hit = false;
    }
}
