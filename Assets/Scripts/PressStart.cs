using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PressStart : MonoBehaviour
{
    [SerializeField] GameObject _introText;
    public bool _isPresskey;

    void Start()
    {
        StartRoutine();
    }

    async void StartRoutine()
    {
        SoundManager.Instance.PlayBGM(SoundManager.ClipBGM.creep);
        while (!Input.anyKey) await Task.Yield();
        _isPresskey = true;
        if (!_isPresskey) return;
        SoundManager.Instance.PlaySFX(SoundManager.ClipSFX.DoorOpen);
        for (int i = 0; i < 3; i++)
        {
            _introText.SetActive(false);
            await Task.Delay(300);
            _introText.SetActive(true);
            await Task.Delay(300);
        }
        await Task.Yield();
        SceneManager.LoadScene("Game");
    }
}
