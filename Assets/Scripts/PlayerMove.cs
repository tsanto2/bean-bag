using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public GameObject DLStart, DRStart, DLEnd, DREnd, RStart, REnd;
    public LayerMask mask;

    public int weapCount;
    public int health;
    public float speed;
    public float jumpForce;
    public Vector3 jumpSpot;
    public Vector3 spawnPoint;
    public bool isGrounded;
    public bool rayRight;
    public bool facingRight;
    public bool dashed;
    public bool dodged;
    public bool canMove;
    public bool onMPlat;
    public bool isKilled;

    public bool canDash;
    public bool canDodge;
    public bool canBoom;
    public bool canShoot;

    public Rigidbody2D rb;
    private DrillSpawn ds;
    private MovingPlatformScript mPlat;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        ds = GameObject.FindObjectOfType<DrillSpawn>();
        mPlat = GameObject.FindObjectOfType<MovingPlatformScript>();
        spawnPoint = transform.position;
        speed = 6.0f;
        jumpForce = 3250.0f;
        facingRight = true;
        dodged = true;
        canMove = true;
        canDash = false;
        weapCount = 0;
        health = 100;
        if (Application.loadedLevelName == "Test")
        {
            canBoom = true;
            canShoot = true;
        }
        if (Application.loadedLevelName == "BossFight")
        {
            canBoom = false;
            canShoot = false;
            canDodge = true;
            canDash = true;
            weapCount = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        RayCast();
        Jump();
        Dash();
        Dodge();
        SwitchWeap();

        if (isKilled)
        {
            transform.position = jumpSpot;
            isKilled = false;
            health -= 10;
        }

        if (health <= 0)
        {
            transform.position = spawnPoint;
            health = 100;
        }

        if (health > 100)
            health = 100;
	}

    void FixedUpdate()
    {
        MoveHoriz();
        if (onMPlat)
        {
            transform.Translate(Vector3.right * 2.25f * Time.deltaTime);
            Vector3 newPos = transform.position;
            newPos.y = mPlat.transform.position.y + .875f;
            transform.position = newPos;
        }
    }

    void RayCast()
    {
        if (Physics2D.Linecast(DLStart.transform.position, DLEnd.transform.position) || Physics2D.Linecast(DRStart.transform.position, DREnd.transform.position))
        {
            dashed = false;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics2D.Linecast(RStart.transform.position, REnd.transform.position, mask))
            rayRight = true;
        else
            rayRight = false;
    }

    void MoveHoriz()
    {
        if (canMove && Input.GetAxis("Horizontal") <= -0.3f)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (canMove && Input.GetAxis("Horizontal") >= 0.3f)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void Dash()
    {
        if (canDash && !dashed && !isGrounded && Input.GetButtonDown("Jump"))
        {
            dashed = true;
            StartCoroutine(DashRight());
        }
       /* if (canDash && !dashed && !isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") <= -0.3f)
        {
            dashed = true;
            StartCoroutine(DashLeft());
        }
        if (canDash && !dashed && !isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Vertical") >= 0.3f)
        {
            dashed = true;
            StartCoroutine(DashUp());
        }
        */
    }

    void Dodge()
    {
        if (canDodge && (Input.GetAxis("LT") > 0) && dodged)
        {
            dodged = false;
            StartCoroutine(DoDodge());
        }
    }

    void SwitchWeap()
    {
        if (Input.GetButtonDown("Y"))
        {
            weapCount += 1;
            if (weapCount == 2 && !canBoom)
            {
                weapCount = 0;
            }
            if (weapCount == 0 && !canShoot)
            {
                weapCount = 1;
            }
            if (weapCount == 3)
                weapCount = 0;
        }
    }

    IEnumerator DashRight()
    {
        Vector2 dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if ((Input.GetAxisRaw("Horizontal") < 0.1f && Input.GetAxisRaw("Horizontal") > -0.1f) && (Input.GetAxisRaw("Vertical") < 0.1f && Input.GetAxisRaw("Vertical") > -0.1f))
            dashDir = Vector2.up;
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        float dashLimitRight = transform.position.x + 4.0f;
        float dashLimitLeft = transform.position.x - 4.0f;
        float dashLimitUp = transform.position.y + 4.0f;
        float dashLimitDown = transform.position.y;
        while (transform.position.x <= dashLimitRight && transform.position.x >= dashLimitLeft && transform.position.y <= dashLimitUp && transform.position.y >= dashLimitDown && !rayRight && !isGrounded)
        {
            transform.Translate(dashDir * Time.deltaTime * speed * 3.5f);
            yield return new WaitForSeconds(0);
        }
        grav.y = -9.81f;
        Physics2D.gravity = grav;
    }

    /*IEnumerator DashLeft()
    {
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = transform.position;
        dashPos.x -= 4.0f;
        while (transform.position.x >= dashPos.x && !rayRight)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 3.5f);
            yield return new WaitForSeconds(0);
        }
        grav.y = -9.81f;
        Physics2D.gravity = grav;
    }

    IEnumerator DashUp()
    {
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = transform.position;
        dashPos.y += 4.0f;
        while (transform.position.y <= dashPos.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * 3.5f);
            yield return new WaitForSeconds(0);
        }
        rb.velocity = new Vector3(0, 0, 0);
        grav.y = -9.81f;
        Physics2D.gravity = grav;
    }*/

    IEnumerator DoDodge()
    {
        canMove = false;
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        this.tag = "Invincible";
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,0f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(0.1f);
        this.tag = "Player";
        grav.y = -9.81f;
        Physics2D.gravity = grav;
        canMove = true;
        yield return new WaitForSeconds(1.5f);
        dodged = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Lava")
        {
            transform.position = spawnPoint;
            Vector3 mPos = mPlat.transform.position;
            mPos.x = 348.5f;
            mPlat.canMove = false;
            mPlat.reset = true;
            mPlat.StopCo();
            mPlat.isMoving = false;
            mPlat.transform.position = mPos;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            health -= 10;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovingPlat")
        {
            onMPlat = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovingPlat")
        {
            onMPlat = false;
        }

        if (col.gameObject.tag == "Platform")
        {
            jumpSpot = this.transform.position;
            if (facingRight)
                jumpSpot.x -= 1.0f;
            else
                jumpSpot.x += 1.0f;
        }
    }
}
