using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

    private PlayerMove player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            player.health += 30;
            Destroy(gameObject);
        }
    }
}
