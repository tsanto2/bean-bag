using UnityEngine;
using System.Collections;

public class DrillSpawn : MonoBehaviour {

    private PlayerMove player;

    public GameObject drill;
    public GameObject drillSpawn;
    public bool isDrill;
    public bool drilled;
    public bool drillDone;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
        isDrill = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.weapCount == 1)
        {
            isDrill = true;
        }
        else {
            isDrill = false;
        }
        if (player.facingRight)
        {
            this.drill.transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            this.drill.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (((Input.GetAxis("RT") > 0) || Input.GetKeyDown(KeyCode.Z)) && isDrill && !drilled)
        {
            drilled = true;
            player.dashed = true;
            Instantiate(this.drill, drillSpawn.transform.position, this.drillSpawn.transform.rotation);
            if (player.facingRight)
                StartCoroutine(DrillRight());
            if (!player.facingRight)
                StartCoroutine(DrillLeft());
        }
        if (Input.GetAxis("RT") < 1)
            if (player.isGrounded)
                drilled = false;
    }

    IEnumerator DrillRight()
    {
        drillDone = false;
        player.rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = player.transform.position;
        dashPos.x += 2.5f;
        while (player.transform.position.x <= dashPos.x && !player.rayRight)
        {
            player.transform.Translate(Vector3.right * Time.deltaTime * player.speed * 3.5f);
            yield return new WaitForSeconds(0);
        }
        //dashPos.y = player.transform.position.y;
        //player.transform.position = dashPos;
        grav.y = -9.81f;
        Physics2D.gravity = grav;
        drillDone = true;
    }

    IEnumerator DrillLeft()
    {
        drillDone = false;
        player.rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = player.transform.position;
        dashPos.x -= 2.5f;
        while (player.transform.position.x >= dashPos.x && !player.rayRight)
        {
            player.transform.Translate(Vector3.left * Time.deltaTime * player.speed * 3.5f);
            yield return new WaitForSeconds(0);
        }
        //dashPos.y = player.transform.position.y;
        //player.transform.position = dashPos;
        grav.y = -9.81f;
        Physics2D.gravity = grav;
        drillDone = true;
    }
}
