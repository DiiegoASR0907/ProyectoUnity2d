using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    Transform target;
    //float tLX, tLY, bRX, bRY;
	// Use this for initialization
	void Awake () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(
        target.position.x,
        target.position.y,
        transform.position.z);
	}
    //public void SetBound (GameObject map)
    //{
    //    //Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
    //    float cameraSize = Camera.main.orthographicSize;

    //    tLX = map.transform.position.x + cameraSize;
    //    tLY = map.transform.position.y - cameraSize;
    //    bRX = map.transform.position.x + cameraSize;
    //    bRY = map.transform.position.y - cameraSize;
    //    //bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
    //    //bRY = map.transform.position.y - config.NumTilesWide - cameraSize;
    //}
} 
