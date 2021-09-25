using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    public float timeOffset = 0.4f;
    public float distanceBetweenTiles = 5.0F;
    public float randomValue = 0.8f;
    private Vector3 previousTilePosition;
    private float startTime;
    public float Space;
    private Vector3 direction, mainDirection,otherDirection;

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // New Vector 3 when creating mainDirection=new Vector3(0, 0, 1) this one is the space its z axis use it to randomize the number

        Space = Random.Range(0.6f, 1.2f);
        mainDirection = new Vector3(0, 0, Space);
        otherDirection = new Vector3(0, 0, 0);








        if (Time.time - startTime > timeOffset)
        {
            if (Random.value < randomValue)
                direction = mainDirection;
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            startTime = Time.time;
              Instantiate(tileToSpawn, spawnPos,Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
        }
    }
}