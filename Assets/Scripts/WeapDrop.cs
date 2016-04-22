using UnityEngine;
using System.Collections;

public class WeapDrop : MonoBehaviour {

    public PlayerMove player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && this.tag == "Boom4Dash")
        {
            player.canDash = true;
            player.canBoom = false;
            if (player.weapCount == 2)
            {
                player.weapCount = 0;
            }
            player.spawnPoint = transform.position;
            player.spawnPoint.y = 0;
            Destroy(gameObject);
        }
        if (col.tag == "Player" && this.tag == "Gun4Dodge")
        {
            player.canDodge = true;
            player.canShoot = false;
            if (player.weapCount == 0)
            {
                player.weapCount = 1;
            }
            player.spawnPoint = transform.position;
            player.spawnPoint.y = 66.0f;
            Destroy(gameObject);
        }
        if (col.tag == "Player" && this.tag == "BossTransition")
        {
            Application.LoadLevel("BossFight");
        }
    }
}
