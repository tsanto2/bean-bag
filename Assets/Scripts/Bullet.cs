using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private PlayerMove player;
    private Rigidbody2D rigidbody;
    private CircleCollider2D collider;
    private BulletSpawn bulletSpawn;

    public int chargeTime;
    public int damage;
    public bool charging;
    public bool isFired;

    // Use this for initialization
    void Start()
    {
        chargeTime = 0;
        player = GameObject.FindObjectOfType<PlayerMove>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<CircleCollider2D>();
        bulletSpawn = GameObject.FindObjectOfType<BulletSpawn>();

        if (Input.GetKey(KeyCode.Z) || (Input.GetAxis("RT")> 0))
        {
            StartCoroutine(ChargeShot());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            transform.position = bulletSpawn.transform.position;
        }
        if (Input.GetKeyUp(KeyCode.Z) || (Input.GetAxis("RT") < 1))
        {
            charging = false;
            if (chargeTime < 10)
            {
                damage = 1;
            }
            if (chargeTime >= 10 && chargeTime < 20)
            {
                damage = 3;
            }
            if (chargeTime >= 20)
            {
                damage = 5;
            }
            if (player.facingRight && !isFired)
            {
                rigidbody.velocity = new Vector2(16.0f, 0);
                isFired = true;
            }
            else if (!player.facingRight && !isFired)
            {
                rigidbody.velocity = new Vector2(-16.0f, 0);
                isFired = true;
            }
        }
        if (!this.gameObject.GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
        if (this.gameObject.GetComponent<Renderer>().isVisible)
        {
        }

    }

    IEnumerator ChargeShot()
    {
        Vector3 t = transform.localScale;
        while ((Input.GetKey(KeyCode.Z) || (Input.GetAxis("RT") > 0)) && chargeTime <= 30)
        {
            charging = true;
            yield return new WaitForSeconds(0.0001f);
            chargeTime++;
            t.x += .003f * chargeTime;
            t.y += .003f * chargeTime;
            transform.localScale = t;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag != "Player") && !charging && col.tag != "Switch")
            Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
