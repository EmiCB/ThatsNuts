using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put generator on end of platform

public class TerrainGenerator : MonoBehaviour {
    public GameObject platform;
    public Transform genPoint;
    public Transform delPoint;

    public ObjectPooler[] platformPoolers;
    private float[] platformWidths;
    private float[] platformHeights;
    private int platformSelector;

    private float distanceBetween;
    private float distBetweenMin;
    private float distBetweenMax;

    public Transform maxHeightPoint;
    private float minHeight;
    private float maxHeight;
    public float maxHeightDifference;
    private float heightDifference;

    private CollectableGenerator collectableGenerator;
    private Vector3 collectablePos;
    private float collectableSelector;

    void Start() {
        platformWidths = new float[platformPoolers.Length];
        platformHeights = new float[platformPoolers.Length];

        for (int i = 0; i < platformPoolers.Length; i++) {
            platformWidths[i] = platformPoolers[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
            platformHeights[i] = platformPoolers[i].pooledObject.GetComponent<BoxCollider2D>().size.y;
        }

        distBetweenMin = 3.0f;
        distBetweenMax = 5.0f;

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        collectableGenerator = FindObjectOfType<CollectableGenerator>();
    }
    void Update() {
        // platforms
        if (transform.position.x < genPoint.position.x) {
            // set x position 
            distanceBetween = Random.Range(distBetweenMin, distBetweenMax);
            platformSelector = Random.Range(0, platformPoolers.Length);

            // set y position
            heightDifference = transform.position.y + Random.Range(-maxHeightDifference, maxHeightDifference);
            if (heightDifference > maxHeight) heightDifference = maxHeight;
            if (heightDifference < minHeight) heightDifference = minHeight;

            // set transform
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);

            // move object
            GameObject newPlatform = platformPoolers[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // spawn objects
            collectablePos = new Vector3(transform.position.x, transform.position.y + (platformHeights[platformSelector] / 2) + 0.5f, transform.position.z);

            collectableSelector = Random.Range(0, 100);
            if (collectableSelector <= 80) collectableGenerator.SpawnAcorns(collectablePos);
            if (collectableSelector > 80) collectableGenerator.SpawnFrenchfries(collectablePos);

            // move point
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), heightDifference, transform.position.z);

        }
    }
}
