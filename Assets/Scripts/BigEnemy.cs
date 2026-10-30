using UnityEngine;

public class BigEnemy : Enemy
{
    public GameObject transformedEnemy;
    public int maxDuplication = 2;
    int checker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        DeathMultiply();
    }

    public void DeathMultiply()
    {
        if (enemyDead == true && checker != maxDuplication)
        {
            for(checker = 0; checker < maxDuplication; checker++)
            {
                Instantiate(transformedEnemy, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)), transform.rotation);
            }
        }
    }
}
