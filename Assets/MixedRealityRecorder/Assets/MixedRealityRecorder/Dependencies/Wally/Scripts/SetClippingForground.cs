﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClippingForground : MonoBehaviour
{

    private Camera cam;
    private Transform player;
        

    // Start is called before the first frame update
    void Start()
    {

        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        cam.farClipPlane = Vector3.Magnitude(player.position - transform.position);

    }
}
