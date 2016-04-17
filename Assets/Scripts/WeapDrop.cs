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
        }
    }
}
