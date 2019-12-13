﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;

    private Vector3 lastPlayerPos;

    private float distToMoveX;

    void Start() {
        player = FindObjectOfType<PlayerController>();
        lastPlayerPos = player.transform.position;
    }
    void Update() {
        distToMoveX = player.transform.position.x - lastPlayerPos.x;

        transform.position = new Vector3(transform.position.x + distToMoveX, transform.position.y, transform.position.z);
        lastPlayerPos = player.transform.position;
    }
}
