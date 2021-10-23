using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    private float spawnX = 0.0f;
    private float tileLenght = 5.0f;
    private float spawnedTiles = 7.0f;
    private float saveZone = 25.0f;
    private float spawnObstacleTimer = 0;
    private float spawnObstacleTimerModifier = 0;
    private List<GameObject> tilesForDelete = new List<GameObject>();
    private List<GameObject> obstaclesForDelete = new List<GameObject>();
    [Header("Дальность спауна препятствий")] public float obstacleSpawnDistance = 15.0f;
    [Header("Максимальная высота спауна препятствий")] public float obstacleSpawnMaxHeight = 2.0f;
    [Header("Минимальная высота спауна препятствий")] public float obstacleSpawnMinHeight = -2.0f;
    [Header("Дальность удаления препятствий")] public float obstacleDestroyDistance = 10.0f;
    void Start()
    {
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        MainSpawn();
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = (GameObject)Instantiate(Resources.Load("Prefabs\\Obstacle"));
        obstacle.transform.position = new Vector2(Camera.main.transform.position.x+Vector2.right.x*obstacleSpawnDistance,Random.Range(obstacleSpawnMinHeight,obstacleSpawnMaxHeight));
        obstaclesForDelete.Add(obstacle);
    }
   
    private void SpawnTile()
    {
        GameObject lines = (GameObject)Instantiate(Resources.Load("Prefabs\\Borders"));
        lines.transform.SetParent(transform);
        lines.transform.position = Vector2.right * spawnX;
        spawnX += tileLenght;
        tilesForDelete.Add(lines);
    }

    private void MainSpawn()
    {
        Spawn();
        ObstaclesSpawn();
        if (FindObjectOfType<BallManager>().Dead)
        {
            Delete(obstaclesForDelete);
        }
    }

    private void Spawn()
    {
        if (Camera.main.transform.position.x - saveZone > (spawnX - spawnedTiles * tileLenght))
        {
            SpawnTile();
            Delete(tilesForDelete);
        }
    }

    private void ObstaclesSpawn()
    {
        ObstaclesControl();
        GameObject obstacle = GameObject.Find("Obstacle(Clone)");
        if (obstacle!= null)
        {
            if (Camera.main.transform.position.x > obstacle.transform.position.x+obstacleDestroyDistance)
            {
                Delete(obstaclesForDelete);
            }
        }
    }

    private void ObstaclesControl()
    {
        if (spawnObstacleTimerModifier == 0)
        {
            spawnObstacleTimerModifier = Random.Range(1,5);
            spawnObstacleTimer = Time.time + spawnObstacleTimerModifier;
        }
        if (Time.time > spawnObstacleTimer)
        {
            SpawnObstacle();
            spawnObstacleTimerModifier = 0;
        }
        
    }
    private void StartSpawn()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnTile();
        }   
    }

    private void Delete(List<GameObject> list)
    {
        if (list.Count > 0)
        {
            Destroy(list[0]);
            list.RemoveAt(0);
        }
    }

}
