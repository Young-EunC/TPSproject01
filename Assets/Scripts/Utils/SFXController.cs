using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : PooledObject
{
    private AudioSource _audioSource;
    private float _currentLength;

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        _currentLength -= Time.deltaTime;
        if (_currentLength <= 0) { ReturnPool(); }
    }
    private void Init() {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Play(AudioClip clip) {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();

        _currentLength = clip.length;
    }
}
