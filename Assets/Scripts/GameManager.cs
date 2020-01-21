using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public enum AudioEvent
{
    GameOver,
    AsteroidDestroyed,
    Shot
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private AudioClip onGameOver;
    [SerializeField] private AudioClip onAsteroidDestroyed;
    [SerializeField] private AudioClip onShot;

    [SerializeField] private PlayableDirector gameOverDirector;

    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TextMeshProUGUI finalTimeLabel;

    private bool isGameOver;

    private void Awake()
    {
        Instance = this;
        ScreenUtils.Initialize();
    }

    private void Start()
    {
        SpawnRock(ScreenUtils.ScreenLeft);
        SpawnRock(ScreenUtils.ScreenRight);
        SpawnRock(ScreenUtils.ScreenTop);
        SpawnRock(ScreenUtils.ScreenBottom);
    }

    private void Update()
    {
        if (isGameOver) return;
        timeLabel.text = $"Time: {Time.time:f2}";
    }

    public void PlaySound(AudioEvent audioEvent, float pitch = 1)
    {
        var audioGameObject = new GameObject();
        var audioSource = audioGameObject.AddComponent<AudioSource>();
        audioSource.pitch = pitch;
        
        switch (audioEvent)
        {
            case AudioEvent.GameOver:
                audioSource.clip = onGameOver;
                break;
            case AudioEvent.AsteroidDestroyed:
                audioSource.clip = onAsteroidDestroyed;
                break;
            case AudioEvent.Shot:
                audioSource.clip = onShot;
                break;
        }
        
        audioSource.Play();
        Destroy(audioGameObject, audioSource.clip.length);
    }

    public GameObject SpawnRock(Vector3 position)
    {
        return Instantiate(asteroidPrefab, position, Quaternion.identity);
    }

    public void GameOver()
    {
        isGameOver = true;
        finalTimeLabel.text = $"Your time: {Time.time:f2}";
        Instance.PlaySound(AudioEvent.GameOver);
        gameOverDirector.Play();
    }
}
