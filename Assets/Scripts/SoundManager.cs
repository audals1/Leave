using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : DontDestroy<SoundManager>
{
    public enum AudioType
    {
        BGM,
        SFX,
        Max
    }
    public enum ClipBGM
    {
        creep,
        Max
    }
    public enum ClipSFX
    {
        DoorOpen,
        Gameover,
        GhostBreath,
        GhostLowBreath,
        GhostLowSound,
        GhostLowSound2,
        GhostScream,
        GhostWisper,
        Max
    }
    [SerializeField]
    AudioClip[] _bgmClips;
    [SerializeField]
    AudioClip[] _sfxClips;
    AudioSource[] _audio;

    int PlayMaxCount = 3;
    Dictionary<ClipSFX, int> _sfxPlayList = new Dictionary<ClipSFX, int>();
    public void PlayBGM(ClipBGM bgm)
    {
        _audio[(int)AudioType.BGM].clip = _bgmClips[(int)bgm];
        _audio[(int)AudioType.BGM].Play();
    }
    public void StopBGM(ClipBGM bgm)
    {
        _audio[(int)AudioType.BGM].clip = _bgmClips[(int)bgm];
        _audio[(int)AudioType.BGM].Stop();
    }
    IEnumerator Coroutine_CheckPlaySFX(ClipSFX sfx, float time)
    {
        yield return new WaitForSeconds(time);
        int count = 0;
        _sfxPlayList.TryGetValue(sfx, out count);
        if (count == 1)
        {
            _sfxPlayList.Remove(sfx);
        }
        else
        {
            _sfxPlayList[sfx]--;
        }

    }
    public void PlaySFX(ClipSFX sfx)
    {
        int count = 0;
        _sfxPlayList.TryGetValue(sfx, out count);
        if (count < PlayMaxCount)
        {
            _audio[(int)AudioType.SFX].PlayOneShot(_sfxClips[(int)sfx]);
            StartCoroutine(Coroutine_CheckPlaySFX(sfx, _sfxClips[(int)sfx].length));
            if (_sfxPlayList.ContainsKey(sfx))
            {
                _sfxPlayList[sfx]++;
            }
            else
            {
                _sfxPlayList.Add(sfx, 1);
            }
        }

    }
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        _audio = new AudioSource[(int)AudioType.Max];

        //초기화과정
        _audio[(int)AudioType.BGM] = gameObject.AddComponent<AudioSource>();
        _audio[(int)AudioType.BGM].loop = true;
        _audio[(int)AudioType.BGM].playOnAwake = false;
        _audio[(int)AudioType.BGM].rolloffMode = AudioRolloffMode.Linear;

        _audio[(int)AudioType.SFX] = gameObject.AddComponent<AudioSource>();
        _audio[(int)AudioType.SFX].loop = false;
        _audio[(int)AudioType.SFX].playOnAwake = false;
        _audio[(int)AudioType.SFX].rolloffMode = AudioRolloffMode.Linear;

        _bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        _sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");
    }
}