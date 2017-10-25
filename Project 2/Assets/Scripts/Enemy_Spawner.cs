using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour {
    public Enemy_BasicController EnemyPrefab;
    public float MinSpawnTIme;
    public float MaxSpawnTime;
    public bool CanSpawn;
    private bool _playerIsClose;
    private float _timer = 0f;
    private float _spawnTime = 0f;
    private Dictionary<Transform, Enemy_BasicController> _enemyPoints = new Dictionary<Transform, Enemy_BasicController>();
    Transform[] children;
	// Use this for initialization
	void Awake () {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child != transform)
            {
                _enemyPoints.Add(child, null);
            }
        }
        _spawnTime = GetRandomSpawnTime();
        _playerIsClose = true;

	}
	
	// Update is called once per frame
	void Update () {
        if (_playerIsClose)
        {
            _timer += Time.deltaTime;
        }
        List<Transform> emptyPoints = new List<Transform>();
        foreach(KeyValuePair<Transform,Enemy_BasicController> keyValuePair in _enemyPoints)
        {
            if(keyValuePair.Value == null)
            {
                emptyPoints.Add(keyValuePair.Key);
            }
        }

        if(_timer >= _spawnTime && emptyPoints.Count >0 && _playerIsClose)
        {
            int randomIndex = Random.Range(0, emptyPoints.Count);
            Transform pointTransform = emptyPoints[randomIndex];
            Vector3 position = pointTransform.position;
            Enemy_BasicController enemy = Instantiate(EnemyPrefab);
            enemy.transform.position = position;

            _enemyPoints[emptyPoints[randomIndex]] = enemy;
            _timer = 0;
            _spawnTime = GetRandomSpawnTime();
        }

        Debug.Log(_playerIsClose);
	}

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(MinSpawnTIme, MaxSpawnTime);
        return spawnTime;
    }

    public Enemy_BasicController GetClosestEnemy (Vector3 position)
    {
        List<Enemy_BasicController> enemies = GetEnemies();
        Enemy_BasicController closest = null;
        float minDistance = float.PositiveInfinity;
        foreach(Enemy_BasicController enemy in enemies)
        {
            float distance = Vector3.Distance(position,enemy.transform.position);
            if(distance < minDistance)
            {
                closest = enemy;
                minDistance = distance;
            }
        }
        return closest;
    }
     private List<Enemy_BasicController> GetEnemies()
    {
         List<Enemy_BasicController> enemies = new List<Enemy_BasicController>();
         foreach(KeyValuePair<Transform,Enemy_BasicController> EnemyPoint in _enemyPoints)
         {
             if(EnemyPoint.Value != null)
             {
                 enemies.Add(EnemyPoint.Value);
             }
         }
         return enemies;
     }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _playerIsClose = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            
            _playerIsClose = false;
        }
    }

}
