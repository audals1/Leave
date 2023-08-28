using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : SingletonMonoBehaviour<Dialog>
{
    GameManager _gameManager;
    [TextArea] public string[] _introtexts;
    [TextArea] public string[] _nametexts;
    [TextArea] public string[] _flowertexts;
    [TextArea] public string[] _flashtexts;
    public TextMeshProUGUI _textUI;
    [SerializeField] int _textIndex;
    public int _itemtextIndex;
    public bool _isDaloging;
    public bool _introDialogFin;
    public bool _DalogFin;

    void Start()
    {
        _textUI = FindObjectOfType<TextMeshProUGUI>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        ShowIntroText(0);
        IntroDialog();
    }
    public void ShowIntroText(int index)
    {
        if(_gameManager._introEventFin && !_introDialogFin)
        {
            _textUI.text = _introtexts[_textIndex];
            _textUI.gameObject.SetActive(true);
        }
    }
    public void IntroDialog()
    {
        if (_gameManager._introEventFin && Input.GetKeyDown(KeyCode.Space) && !_introDialogFin)
        {
            if (_textIndex < _introtexts.Length - 1) // 대화창 진행중
            {
                _isDaloging = true;
                _textIndex++;
                _textUI.text = _introtexts[_textIndex];
            }
            else //대화창 다 돌음
            {
                _introDialogFin = true;
                _isDaloging = false;
                _textUI.gameObject.SetActive(false);
            }
        }
    }
    public void ShowFlowerText(int index)
    {
        _textUI.gameObject.SetActive(true);
        _textUI.text = _flowertexts[index];
    }
    public void ShowFlashText(int index)
    {
        _textUI.gameObject.SetActive(true);
        _textUI.text = _flowertexts[index];
    }
    public void ShowNameTagText(int index)
    {
        _textUI.gameObject.SetActive(true);
        _textUI.text = _nametexts[index];
    }
}
