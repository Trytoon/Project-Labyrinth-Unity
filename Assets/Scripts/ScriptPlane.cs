using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPlane : MonoBehaviour
{
    public Transform PosCamera;

    WebCamTexture WebCamTexture;
    public string path;
    public RawImage imgDisplayForPhone;
    void Start()
    {
        WebCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = WebCamTexture;
        WebCamTexture.Play();

        this.transform.localScale = new Vector3(0.55f, 0.1f, 0.25f);
        this.transform.rotation = new Quaternion(0.0f, 1.0f, -0.1f, 0.0f);
    }

    void Update()
    {
        this.transform.position = new Vector3(0.0f, -2f, 0.6f) + PosCamera.transform.position;
    }

    
}
