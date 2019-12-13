using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour {

    public ObjectPooler acornPool;
    public ObjectPooler frenchfryPool;

    public void SpawnAcorns(Vector3 startPosition) {
        GameObject acorn = acornPool.GetPooledObject();
        acorn.transform.position = startPosition;
        acorn.SetActive(true);
    }

    public void SpawnFrenchfries(Vector3 startPosition) {
        GameObject frenchfry = frenchfryPool.GetPooledObject();
        frenchfry.transform.position = startPosition;
        frenchfry.SetActive(true);
    }
}
