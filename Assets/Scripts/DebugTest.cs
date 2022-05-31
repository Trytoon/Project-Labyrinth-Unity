using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTest : MonoBehaviour
{

    public Text t;
    public GameObject g;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.text = " "+g.transform.position;
    }
}
