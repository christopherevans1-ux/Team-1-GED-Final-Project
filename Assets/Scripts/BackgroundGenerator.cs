using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{     
    public GameObject[] shapePrefabs;
    public int shapeCount = 100;

    public Vector2 minBounds = new Vector2(-10 , 6);
    public Vector2 maxBounds = new Vector2(200, 10);
    [SerializeField] Transform backgroundHolder;


    void Start()
    {
        for (int i = 0; i < shapeCount; i++)
        {
           Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
           GameObject shape = shapePrefabs[Random.Range(0, shapePrefabs.Length)];
           Instantiate(shape, pos, Quaternion.identity, backgroundHolder);
        }
    }
}