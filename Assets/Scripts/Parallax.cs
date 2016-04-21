using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    private Transform cam;
    private Vector3 prevCamPos;

    public Transform[] backgrounds;
    private float[] scales;
    public float smoothing = 1.5f;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
        prevCamPos = cam.position;
        scales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            scales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (prevCamPos.x - cam.position.x) * scales[i];
            float targetPosX = backgrounds[i].position.x + parallax;
            Vector3 targetPos = new Vector3(targetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smoothing * Time.deltaTime);
        }
        prevCamPos = cam.position;
	}
}
