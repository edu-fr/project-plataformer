using System;
using System.Collections;
using System.Collections.Generic;
using ProjectPlataformer;
using Unity.VisualScripting;
using UnityEngine;

public class LevelPartScript : MonoBehaviour
{
    // Start is called before the first frame update
    private player playerRef;
    private bool isGoingToDestroyItself = false;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
    }

    void Update()
    {
        if (isGoingToDestroyItself) return;
        
        if (playerRef.isRunning)
        {
            Destroy(gameObject, 15f);
            isGoingToDestroyItself = true;
        }
    }
}
