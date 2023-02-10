using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSoundManager;

public class GameSoundManager : SingletonMonoBehaviour<GameSoundManager>
{
    // BGMを再生するAudioSource
    private AudioSource BGMAudioSource;

    // SEを再生するAudioSource
    private AudioSource[] SEAudioSources = new AudioSource[3];

    public List<AudioClip> BGMAudioClips = new List<AudioClip>();

    public List<AudioClip> SEAudioClips = new List<AudioClip>();

    public enum BGMTypes
    {
        Invalide = -1,
        GameStart,
        GameMain,
        GameResult,
    }
    public BGMTypes BGMType;


    public enum SETypes
    {
        Invalide = -1,
        CardOpen,
        CardClose,
    }
    public SETypes SEType;


    public void Initialize()
    {
        BGMAudioSource = this.gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < SEAudioSources.Length; i++)
        {
            SEAudioSources[i] = this.gameObject.AddComponent<AudioSource>();
        }

    }

    public void SetBGMAudioClips(AudioClip bgmAudio)
    {
        Debug.Log(bgmAudio);
        BGMAudioClips.Add(bgmAudio);
    }

    public void SetSEAudioClips(AudioClip seAudio)
    {
        Debug.Log(seAudio);
        SEAudioClips.Add(seAudio);
    }


    public void PlayBGM(BGMTypes bGMType)
    {
        // Nullチェック
        if (BGMAudioClips[(int)bGMType] == null)
        {
            return;
        }
        BGMAudioSource.clip = BGMAudioClips[(int)bGMType];
        BGMAudioSource.Play();
    }

    public void PlaySE(SETypes seType)
    {
        // Nullチェック
        if (SEAudioClips[(int)seType] == null)
        {
            return;
        }
        foreach (var seAudio in SEAudioSources)
        {
            if (!seAudio.isPlaying)
            {
                //SEAudioSourceは複数のスピーカー。seAudioはその内の一つ。このスピーカーが鳴ってなかったらそれを選んで音を鳴らす。
                seAudio.clip = SEAudioClips[(int)seType];
                seAudio.Play();
                break;
            }
        }
    }

}
