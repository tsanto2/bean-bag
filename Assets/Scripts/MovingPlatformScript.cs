using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {

    public bool canMove;
    public bool isMoving;
    public bool isUnder;
    public bool reset;

	// Use this for initialization
	void Start () {
        StartCoroutine(VertPlatMotion());
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void StopCo()
    {
        StopCoroutine("HorizPlatMotion");
    }

    public IEnumerator HorizPlatMotion()
    {
        if (!reset)
            isMoving = true;
        if (transform.position.x <= 465.0f && canMove && !reset)
        {
            transform.Translate(Vector3.right * 2.25f * Time.deltaTime);
            yield return new WaitForSeconds(0);
            StartCoroutine(HorizPlatMotion());
        }
    }

    IEnumerator VertPlatMotion()
    {
        while (transform.position.y >= 63.45f)
        {
            transform.Translate(Vector3.down * 1f * Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
        while (transform.position.y <= 64.5f)
        {
            transform.Translate(Vector3.up * 1f * Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(.75f);
        StartCoroutine(VertPlatMotion());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canMove = true;
            reset = false;
            if (canMove && !isMoving)
            {
                StartCoroutine(HorizPlatMotion());
            }
        }
    }
}
