using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public List<EnemyScriptableBase> enemyTypes;
    public List<Vector3> spawnPoints;

    [SerializeField] private float _spawnTimerMax;
    private float _spawnTimer;
    private float _timer;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnGameStart.AddListener(InitialSpawn);
        EventManager.OnGameEnd.AddListener(() => StopAllCoroutines());
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnGameStart.RemoveListener(InitialSpawn);
        EventManager.OnGameEnd.RemoveListener(() => StopAllCoroutines());
    }

    private void InitialSpawn()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnEnemy();
        }
        _timer = 0;
        _spawnTimer = _spawnTimerMax;
        StartCoroutine(SpawnTimerDecreaseCo());
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
            return;
        _timer += Time.deltaTime;

        if (_timer < _spawnTimer)
            return;
        _timer = 0;
        SpawnEnemy();
    }

    private IEnumerator SpawnTimerDecreaseCo()
    {
        while (_spawnTimer > 0.1f)
        {
            yield return new WaitForSeconds(10f);
            _spawnTimer -= 0.1f;
        }
        yield return new WaitForSeconds(0f);
    }

    void SpawnEnemy()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        GameObject temp = Instantiate(characterPrefab, spawnPoints[randomSpawnPoint], Quaternion.identity);
        int randomEnemyType = Random.Range(0, enemyTypes.Count);
        temp.GetComponent<CharacterInitializer>().Initialize(enemyTypes[randomEnemyType]);
    }

}
