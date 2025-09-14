using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip bgmAudio;
    [SerializeField] AudioClip tapAudio;
    [SerializeField] AudioClip uiButtonAudio;
    private  AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmAudio;
        audioSource.Play();
    }

    public void PlayTap(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(tapAudio,position);
    }

    public void PlayButtonTap(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(uiButtonAudio, position);
    }

    private void OnDestroy()
    {
        audioSource.clip = null;
    }
}
