using UnityEngine;

public class BasicESpawner : EnemySpawner
{
    public GameObject basicEnemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Enemy SpawnEnemy()
    {
        GameObject SpawnedEnemy = Instantiate(basicEnemyPrefab, transform.position, Quaternion.identity);
        return SpawnedEnemy.GetComponent<Enemy>();
    }
}
