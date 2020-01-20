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
        Instantiate(asteroidPrefab, ScreenUtils.ScreenLeft, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenRight, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenTop, Quaternion.identity);
        Instantiate(asteroidPrefab, ScreenUtils.ScreenBottom, Quaternion.identity);
    }

    public void PlaySound(AudioEvent audioEvent)
    {
        var audioGameObject = new GameObject();
        var audioSource = audioGameObject.AddComponent<AudioSource>();
        
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
}
