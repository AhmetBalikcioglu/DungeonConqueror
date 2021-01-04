using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public List<EnemyScriptableBase> enemyTypes;
    public List<Vector3> spawnPoints;

    [SerializeField] private float _spawnTimer;
    private float timer;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnGameStart.AddListener(InitialSpawn);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnGameStart.RemoveListener(InitialSpawn);

    }

    private void InitialSpawn()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnEnemy();
        }
        timer = 0;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
            return;
        timer += Time.deltaTime;
        if (timer < _spawnTimer)
            return;
        timer = 0;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        GameObject temp = Instantiate(characterPrefab, spawnPoints[randomSpawnPoint], Quaternion.identity);
        int randomEnemyType = Random.Range(0, enemyTypes.Count);
        temp.GetComponent<CharacterInitializer>().Initialize(enemyTypes[randomEnemyType]);
    }

}
