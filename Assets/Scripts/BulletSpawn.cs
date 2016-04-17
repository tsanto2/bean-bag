using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour {

    public GameObject bullet, bulletSpawn;
    public int chargeTime;
    public bool fired;
    public bool isGun;

    private PlayerMove player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMove>();
        chargeTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.weapCount == 0)
            isGun = true;
        else
            isGun = false;

	    if ((Input.GetKeyDown(KeyCode.Z) || (Input.GetAxis("RT") > 0)) && !fired && isGun)
        {
            fired = true;
            Instantiate(this.bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        }
        if (Input.GetKeyUp(KeyCode.Z) || (Input.GetAxis("RT") < 1))
        {
            fired = false;
        }
	}
}
