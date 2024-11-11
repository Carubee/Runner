using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : NetworkBehaviour
{
    public GameObject circlePrefabRed;
    public GameObject circlePrefabBlue;
    private Vector3 randomPosition;
    public static CircleManager singleton;
    public Vector3 lastSpawn;

    public GameObject[] spawnSurface;
    public List<Vector3> SpawnPool = new List<Vector3>();
    public List<Vector3> SpawnPool2 = new List<Vector3>();

    Collider surfaceCollider;
    Collider surfaceColliderLv2;
    Collider surfaceColliderLv3;

    int maxSpawn = 30;
    void Start()
    {
        singleton = this;

        surfaceCollider = spawnSurface[0].GetComponent<Collider>();
        surfaceColliderLv2 = spawnSurface[1].GetComponent<Collider>();
        surfaceColliderLv3 = spawnSurface[2].GetComponent<Collider>();

        for (int i = 0; i < maxSpawn; i++)
        {
            if (i <= 5)
            {
                Vector3 randomPosition = GetRandomPositionOnSurface(surfaceCollider);
                SpawnPool.Add(randomPosition);
                SpawnPool2.Add(randomPosition);
            }
            else if (i >= 5 && i <= 10)
            {
                Vector3 randomPosition = GetRandomPositionOnSurface(surfaceColliderLv2);
                SpawnPool.Add(randomPosition);
                SpawnPool2.Add(randomPosition);
            }
            else if (i >= 10)
            {
                Vector3 randomPosition = GetRandomPositionOnSurface(surfaceColliderLv3);
                SpawnPool.Add(randomPosition);
                SpawnPool2.Add(randomPosition);
            }
            Debug.Log(randomPosition);


        }
        //GenerateNewCircle("Cube(Clone)", 1, false);
        //GenerateNewCircle("Cube(Clone)", 2, false);

    }
    Vector3 GetRandomPositionOnSurface(Collider surface)
    {
        Bounds bounds = surface.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, 4.9f, randomZ);
    }
    public void GenerateNewCircle(string name, int site, bool timeOut)
    {
        Debug.Log("New Gen");
        // สุ่มตำแหน่งภายในขอบเขตห้อง
        float z = randomPosition.z - lastSpawn.z;
        if (timeOut)
        {
            if (site == 1)
            {
                randomPosition = SpawnPool[0];
            }
            else if (site == 2)
            {
                randomPosition = SpawnPool2[0];
                randomPosition.x *= -1;
            }
        }
        else
        {
            if (site == 1)
            {
                randomPosition = SpawnPool[0];
                SpawnPool.RemoveAt(0);

            }
            else if (site == 2)
            {
                randomPosition = SpawnPool2[0];
                randomPosition.x *= -1;
                SpawnPool2.RemoveAt(0);

            }
        }
        /*if (lastSpawn.z == 0)
        {
            randomPosition = new Vector3(0, 6, UnityEngine.Random.Range(-17, 0.5f));
        }
        else if (lastSpawn.z < 0 && lastSpawn.z >= -4)
        {
            randomPosition = new Vector3(0, 6, Random.Range(-17, -8));
        }
        else if (lastSpawn.z < -4 && lastSpawn.z >= -8)
        {
            randomPosition = new Vector3(0, 6, Random.Range(-17, -12));
        }
        else if (lastSpawn.z < -8 && lastSpawn.z >= -12)
        {
            randomPosition = new Vector3(0, 6, Random.Range(-4, 0));
        }
        else if (lastSpawn.z < -12 && lastSpawn.z >= -17)
        {
            randomPosition = new Vector3(0, 6, Random.Range(-8, 0));
        }

        lastSpawn = randomPosition;*/
        Debug.Log(name);
        GameObject existingCube = GameObject.Find(name);

        if (site == 1)
        {
            if (existingCube == null)
            {
                GameObject projectile = Instantiate(circlePrefabRed, randomPosition, Quaternion.identity);
                NetworkServer.Spawn(projectile);
            }
            else
            {
                Debug.Log("Found");
               NetworkServer.Destroy(existingCube);
                GameObject projectile = Instantiate(circlePrefabRed, randomPosition, Quaternion.identity);
                NetworkServer.Spawn(projectile);

            }

        }
        else
        {
            if (existingCube == null)
            {
                GameObject projectile = Instantiate(circlePrefabBlue, randomPosition, Quaternion.identity);
                NetworkServer.Spawn(projectile);
            }
            else
            {
                Debug.Log("Found");
                NetworkServer.Destroy(existingCube);
                GameObject projectile = Instantiate(circlePrefabBlue, randomPosition, Quaternion.identity);
                NetworkServer.Spawn(projectile);
            }
        }
    }

}
