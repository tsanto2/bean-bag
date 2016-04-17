using UnityEngine;
using System.Collections;

public class DrillScript : MonoBehaviour {
    private DrillSpawn drillSpawn;
    private PlayerMove player;

    // Use this for initialization
    void Start()
    {
        drillSpawn = GameObject.FindObjectOfType<DrillSpawn>();
        player = GameObject.FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RT") > 0)
        {
            transform.position = drillSpawn.transform.position;
            if (player.facingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (Input.GetAxis("RT") < 1 || drillSpawn.drillDone)
        {
            Destroy(gameObject);
        }
    }
}
