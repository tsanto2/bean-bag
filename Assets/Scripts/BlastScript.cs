using UnityEngine;
using System.Collections;

public class BlastScript : MonoBehaviour {

    private BossAI boss;
    private PlayerMove player;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        boss = GameObject.FindObjectOfType<BossAI>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (boss.facingRight == true)
        {
            rigidbody.velocity = new Vector2(10.0f, 0);
        }
        if (boss.facingRight == false)
        {
            rigidbody.velocity = new Vector2(-10.0f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerMove>().health -= 10;
            Destroy(gameObject);
        }
        if (col.tag != "Player")
        {
            //Destroy (gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
