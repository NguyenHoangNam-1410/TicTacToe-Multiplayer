using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Transform placeSfxPrefab;
    [SerializeField] private Transform winSfxPrefab;
    [SerializeField] private Transform loseSfxPrefab;
    [SerializeField] private Transform gameSfxPrefab;

    private AudioSource gameSfxAudioSource;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnPlacedObject += GameManager_OnPlacedObject;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;
        GameManager.Instance.OnRematch += GameManager_OnRematch; // Subscribe to OnRematch
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        // Instantiate the game sound effect and store its AudioSource
        Transform sfxTransform = Instantiate(gameSfxPrefab);
        gameSfxAudioSource = sfxTransform.GetComponent<AudioSource>();
    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        // Pause the game sound effect
        if (gameSfxAudioSource != null)
        {
            gameSfxAudioSource.Pause();
        }

        // Play win or lose sound effect
        if (GameManager.Instance.GetLocalPlayerType() == e.winPlayerType)
        {
            Transform sfxTransform = Instantiate(winSfxPrefab);
            Destroy(sfxTransform.gameObject, 5f);
        }
        else
        {
            Transform sfxTransform = Instantiate(loseSfxPrefab);
            Destroy(sfxTransform.gameObject, 5f);
        }
    }

    private void GameManager_OnRematch(object sender, EventArgs e)
    {
        // Resume the game sound effect
        if (gameSfxAudioSource != null)
        {
            gameSfxAudioSource.UnPause();
        }
    }

    private void GameManager_OnPlacedObject(object sender, EventArgs e)
    {
        Transform sfxTransform = Instantiate(placeSfxPrefab);
        Destroy(sfxTransform.gameObject, 5f);
    }
}
