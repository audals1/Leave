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
        GhostBreath,
        GhostLowBreath,
        GhostLowSound,
        GhostLowSound2,
        GhostScream,
        GhostWisper,
        Max
    }
    [SerializeField]
    AudioClip[] m_bgmClips;
    [SerializeField]
    AudioClip[] m_sfxClips;
    AudioSource[] m_audio;

    int PlayMaxCount = 3;
    Dictionary<ClipSFX, int> m_sfxPlayList = new Dictionary<ClipSFX, int>();
    public void PlayBGM(ClipBGM bgm)
    {
        m_audio[(int)AudioType.BGM].clip = m_bgmClips[(int)bgm];
        m_audio[(int)AudioType.BGM].Play();
    }
    public void StopBGM(ClipBGM bgm)
    {
        m_audio[(int)AudioType.BGM].clip = m_bgmClips[(int)bgm];
        m_audio[(int)AudioType.BGM].Stop();
    }
    IEnumerator Coroutine_CheckPlaySFX(ClipSFX sfx, float time)
    {
        yield return new WaitForSeconds(time);
        int count = 0;
        m_sfxPlayList.TryGetValue(sfx, out count);
        if (count == 1)
        {
            m_sfxPlayList.Remove(sfx);
        }
        else
        {
            m_sfxPlayList[sfx]--;
        }

    }
    public void PlaySFX(ClipSFX sfx)
    {
        int count = 0;
        m_sfxPlayList.TryGetValue(sfx, out count);
        if (count < PlayMaxCount)
        {
            m_audio[(int)AudioType.SFX].PlayOneShot(m_sfxClips[(int)sfx]);
            StartCoroutine(Coroutine_CheckPlaySFX(sfx, m_sfxClips[(int)sfx].length));
            if (m_sfxPlayList.ContainsKey(sfx))
            {
                m_sfxPlayList[sfx]++;
            }
            else
            {
                m_sfxPlayList.Add(sfx, 1);
            }
        }

    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_audio = new AudioSource[(int)AudioType.Max];

        //�ʱ�ȭ����
        m_audio[(int)AudioType.BGM] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.BGM].loop = true;
        m_audio[(int)AudioType.BGM].playOnAwake = false;
        m_audio[(int)AudioType.BGM].rolloffMode = AudioRolloffMode.Linear;

        m_audio[(int)AudioType.SFX] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.SFX].loop = false;
        m_audio[(int)AudioType.SFX].playOnAwake = false;
        m_audio[(int)AudioType.SFX].rolloffMode = AudioRolloffMode.Linear;

        m_bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        m_sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");
    }
}