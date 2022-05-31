using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = GameObject.Find("Player").transform.position;
        position.y += 1;
        rotation = new Quaternion(0.6f, 0f, 0f, 0.8f);

        this.transform.position = position;
        this.transform.rotation = rotation;
    }
}
