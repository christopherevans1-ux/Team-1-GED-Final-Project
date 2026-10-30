using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] platforms;
    [SerializeField] Transform platformHolder;
    [SerializeField] private float platformSpacing = 18f;

    private void Start()
    {
        int levelLength = Random.Range(15, 21);

        Vector3 currentPos = Vector3.zero;

        for (int i = 0; i < levelLength; i++)
        {
            GameObject room = Instantiate(platforms[Random.Range(0, platforms.Length)], currentPos, Quaternion.identity, platformHolder);
            currentPos.x += platformSpacing;
        }
    }
}