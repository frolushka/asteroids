using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;

    private void Awake()
    {
        ScreenUtils.Initialize();
    }

    private void Start()
    {
        Instantiate(asteroidPrefab, ScreenUtils.ScreenLeft, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenRight, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenTop, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenBottom, Quaternion.identity);
    }
}
