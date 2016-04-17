using UnityEngine;
using System.Collections;

public class BoomSpawn : MonoBehaviour {

    public GameObject boomerang;
    public GameObject boomSpawn;
    public GameObject boom;
    public bool isBoom;
    public bool isActive;
    public bool rDown;

    private PlayerMove player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
        isBoom = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.weapCount == 2)
        {
            isBoom = true;
        }
        else {
            isBoom = false;
        }

        if ((Input.GetAxis("RT") > 0) && isBoom && !isActive)
        {
            rDown = true;
            Instantiate(this.boomerang, boomSpawn.transform.position, this.boomSpawn.transform.rotation);
            isActive = true;
        }

        if (Input.GetAxis("RT") < 1)
        {
            rDown = false;
        }
    }
}
