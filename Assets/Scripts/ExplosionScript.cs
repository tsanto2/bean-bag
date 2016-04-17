using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
