using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicHandler : MonoBehaviour
{
    public bool musicPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!musicPlaying)
            {
                musicPlaying = true;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                musicPlaying = false;
                GetComponent<AudioSource>().Stop();
            }
        }


       
    }

}
