using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TextAppearAnimation : MonoBehaviour
{
    [SerializeField] private List<Data_TextAppearAnimation> _data = new List<Data_TextAppearAnimation>();
    private char _temporatyLetter;
    private int _index = 0;
    private int _countOfCoroutines = 0;
    private int _currentCoroutinesCount = 0;
    private void Start() => StartCoroutines();
    public IEnumerator TextAnimation(Data_Text data)
    {
        yield return new WaitForSeconds(data.WhenToStartWriting);
        for (int i = 0; i < data.Texts.Count; i++)
        {
            for (int j = 0; j < data.Texts[i].Length; j++)
            {
                _temporatyLetter = data.Texts[i][j];
                data.Tmp.text += _temporatyLetter;
                yield return new WaitForSeconds(data.Delay);
            }
            data.Tmp.text += "\n";
            data.Delay = Mathf.Clamp(data.Delay / 1.4f, 0.01f, 10);
        }
        if (data.IsSync)
        {
            _currentCoroutinesCount++;
            if(_currentCoroutinesCount == _countOfCoroutines)
                StartCoroutines();
        }
    }
    private void StartCoroutines()
    {
        if (_index < _data.Count)
        {
            _countOfCoroutines = 0;
            _currentCoroutinesCount = 0;

            for (int i = 0; i < _data[_index].Texts.Count; i++)
            {
                if(_data[_index].Texts[i].IsSync)
                    _countOfCoroutines++;

                StartCoroutine(TextAnimation(_data[_index].Texts[i]));
            }
            _index++;
        }
    }
}



[System.Serializable]
public class Data_TextAppearAnimation
{
    public List<Data_Text> Texts = new List<Data_Text>();
}

[System.Serializable]
public class Data_Text
{
    public bool IsSync = true;
    public TMP_Text Tmp;
    public List<string> Texts = new List<string>();
    public float Delay = .05f;
    public float WhenToStartWriting = 1f;
}











/*
 
 
 using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TextAppearAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private List<string> _texts = new List<string>();
    [SerializeField] private float _delay = .05f;
    [SerializeField] private float _whenToStartWriting = 1f;
    private char _temporatyLetter;

    private void Start() => Invoke(nameof(StartTextWriting), _whenToStartWriting);
    private void StartTextWriting() => StartCoroutine(TextAnimation(_texts, _tmpText, _delay));

    public IEnumerator TextAnimation(List<string> texts, TMP_Text tmp, float delay)
    {
        for (int i = 0; i < texts.Count; i++)
        {
            for (int j = 0; j < texts[i].Length; j++)
            {
                _temporatyLetter = texts[i][j];
                tmp.text += _temporatyLetter;
                yield return new WaitForSeconds(delay);
            }
            tmp.text += "\n";
            delay = Mathf.Clamp(delay / 1.4f, 0.01f, 10);
        }
        yield return null;
    }
}
 
 
 */