using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singletone<GameManager>
{
    private AudioSource _bgmSource;


    private void Awake()
    {
        Init();
    }
    private void Init() 
    {
        _bgmSource = GetComponent<AudioSource>();
    }
    public void BgmPlay() 
    {
        _bgmSource.Play();
    }
}
