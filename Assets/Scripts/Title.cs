using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Title : MonoBehaviour
{
    [SerializeField] TitleText _titleText;
    public bool _isPresskey;

    void Awake()
    {
        _titleText = FindObjectOfType<TitleText>();
    }

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
            _titleText.gameObject.SetActive(false);
            await Task.Delay(300);
            _titleText.gameObject.SetActive(true);
            await Task.Delay(300);
        }
        await Task.Yield();
        SceneManager.LoadScene("Game");
    }
}
