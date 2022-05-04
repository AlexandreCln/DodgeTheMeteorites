using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]private GameObject _projectilePrefab;
    [SerializeField]private float _timeBetweenSpawnMin = .33f;
    [SerializeField]private float _timeBetweenSpawnMax = .8f;
    [SerializeField]private float _timeToReachMaxDifficulty = 120f;
    [SerializeField]private float _angleMax = 15f;
    [SerializeField]private (float, float) _sizeMinMax = (1f, 4f);
    private Vector2 screenHalfSizeWorldUnits;
    private float _newSpawnTime;
    private float _timeBetweenSpawn;

    void Start()
    {
        // stores both x and y values in a Vector2 to access group them easily (x and y is meaningfull here also)
        screenHalfSizeWorldUnits = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
    }

    private void Update()
    {
        if (Time.time < _timeToReachMaxDifficulty)
            _timeBetweenSpawn = Mathf.Lerp(_timeBetweenSpawnMax, _timeBetweenSpawnMin, Time.time / _timeToReachMaxDifficulty);

        if (Time.time > _newSpawnTime)
        {
            _newSpawnTime = Time.time + _timeBetweenSpawn;
            float projectileSize = Random.Range(_sizeMinMax.Item1 , _sizeMinMax.Item2);
            Vector2 spawnPos = new Vector2(
                Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), 
                screenHalfSizeWorldUnits.y + (projectileSize/4)
            );
            // random spawn angle, oriented to the center
            float absAngle = Mathf.Abs(Random.Range(-_angleMax, _angleMax));
            float spawnAngle = (spawnPos.x < 0) ? absAngle : -absAngle;
            Quaternion rotation = Quaternion.Euler(0, 0, spawnAngle);
            GameObject projectile = Instantiate(_projectilePrefab, spawnPos, rotation);
            projectile.transform.localScale = Vector2.one * projectileSize;
        }
    }
}
