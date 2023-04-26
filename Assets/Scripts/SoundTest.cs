using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField]AudioClip m_clip;
    AudioSource m_audio;
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_audio.clip = m_clip;
        m_audio.playOnAwake = false;
        m_audio.loop = true;
        Invoke("Play", 0.0f);
    }
    IEnumerator Coroutin_BGM()
    {
        yield return new WaitForSeconds(3.5f);
        if (Dialog.Instance.m_introDialogFin)
        {
            m_audio.Play();
        }
    }
    void Play()
    {
        m_audio.Play();
    }
}
