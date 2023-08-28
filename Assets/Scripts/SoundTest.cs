using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField]AudioClip _clip;
    AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _clip;
        _audio.playOnAwake = false;
        _audio.loop = true;
        Invoke("Play", 0.0f);
    }
    IEnumerator Coroutin_BGM()
    {
        yield return new WaitForSeconds(3.5f);
        if (Dialog.Instance._introDialogFin)
        {
            _audio.Play();
        }
    }
    void Play()
    {
        _audio.Play();
    }
}
