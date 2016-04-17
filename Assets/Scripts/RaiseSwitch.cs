using UnityEngine;
using System.Collections;

public class RaiseSwitch : MonoBehaviour {

    public GameObject raiseWall;
    public bool isHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Explosion" || col.tag == "Bullet") && !isHit)
        {
            isHit = true;
            StartCoroutine(RaiseWall());
        }
    }

    IEnumerator RaiseWall()
    {
        Vector3 wallPos = raiseWall.transform.position;
        wallPos.y += 2.0f;
        while (raiseWall.transform.position.y <= wallPos.y)
        {
            raiseWall.transform.Translate(Vector3.right * Time.deltaTime * 0.5f);
            yield return new WaitForSeconds(0);
        }
    }
}
