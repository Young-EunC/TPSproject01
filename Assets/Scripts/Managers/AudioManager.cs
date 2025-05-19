using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singletone<GameManager>
{
    private AudioSource _bgmSource;
    private ObjectPool _sfxPool;
    [SerializeField] private List<AudioClip> _bgmList = new();
    [SerializeField] private PooledObject _sfxPrefab;

    private void Awake()
    {
        Init();
    }
    private void Init() 
    {
        _bgmSource = GetComponent<AudioSource>();
        _sfxPool = new ObjectPool(_sfxPrefab, gameObject.transform, 10);
    }
    public void BgmPlay(int idx) 
    {
        if (idx >= 0 && idx < _bgmList.Count)
        {
            _bgmSource.Stop();
            _bgmSource.clip = _bgmList[idx];
            _bgmSource.Play();
        }
    }

    public SFXController GetSFX() {
        PooledObject po = _sfxPool.Get();
        return po as SFXController;
    }
}
