using System;
using UnityEngine;

public enum AudioEvent
{
    PlayerDestroyed,
    AsteroidDestroyed,
    Shot
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private AudioClip onPlayerDestroyed;
    [SerializeField] private AudioClip onAsteroidDestroyed;
    [SerializeField] private AudioClip onShot;

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

    public void PlaySound(AudioEvent audioEvent, float pitch = 1)
    {
        var audioGameObject = new GameObject();
        var audioSource = audioGameObject.AddComponent<AudioSource>();
        audioSource.pitch = pitch;
        
        switch (audioEvent)
        {
            case AudioEvent.PlayerDestroyed:
                audioSource.clip = onPlayerDestroyed;
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
}
