using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorRandomizer : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Start()
    {
        // ; These 5 lines set background camera to random color
        cam = GetComponent<Camera>();
        float randomRcolor = Random.Range(0.1f, 1f);
        float randomGcolor = Random.Range(0.3f, 0.8f);
        float randomBcolor = Random.Range(0.1f, 1f);
        cam.backgroundColor = new Color(randomRcolor, randomGcolor, randomBcolor);
    }
}
