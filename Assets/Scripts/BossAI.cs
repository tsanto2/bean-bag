using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

    public GameObject blast;
    public Transform blastSpawn;

    private PlayerMove player;

    public float speed;
    public float health;
    public bool walkRight;
    public bool flashing;
    public bool facingRight;
    public int shotCount;

    private SpriteRenderer sr;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        
        shotCount = 30;
        speed = 5.0f;
        health = 100.0f;
        walkRight = false;
        facingRight = false;
        flashing = false;
        StartCoroutine(RightSidePhase());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shotCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            shotCount -= col.GetComponent<Bullet>().damage;
            if (!flashing)
                StartCoroutine(hitFlash());
        }
        if (col.tag == "Boom")
        {
            if (!flashing)
                StartCoroutine(hitFlash());
            shotCount = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Drill")
        {
            shotCount -= 5;
            if (!flashing)
                StartCoroutine(hitFlash());
        }
    }

    IEnumerator RightSidePhase()
    {
        facingRight = false;
        yield return new WaitForSeconds(1.0f);
        transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * 1.5f);
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.75f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(2.0f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(2.0f);
        rigidbody.AddForce(Vector2.up * 10000.0f);
        yield return new WaitForSeconds(3.0f);
        rigidbody.AddForce(Vector2.up * 10000.0f);
        while (this.transform.position.x > -11)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 5.0f);
            yield return new WaitForSeconds(0.00001f);
        }
        StartCoroutine(LeftSidePhase());
    }

    IEnumerator LeftSidePhase()
    {
        facingRight = true;
        yield return new WaitForSeconds(1.0f);
        transform.localScale = new Vector3(-1, 1, 1);
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * 1.5f);
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.75f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(2.0f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(0.6f);
        Instantiate(this.blast, blastSpawn.position, blastSpawn.rotation);
        yield return new WaitForSeconds(2.0f);
        rigidbody.AddForce(Vector2.up * 5000.0f);
        yield return new WaitForSeconds(3.0f);
        rigidbody.AddForce(Vector2.up * 5000.0f);
        while (this.transform.position.x < 10.1)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * 5.0f);
            yield return new WaitForSeconds(0.00001f);
        }
        StartCoroutine(RightSidePhase());
    }

    IEnumerator hitFlash()
    {
        flashing = true;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        sr.color = Color.white;
        flashing = false;
    }
}