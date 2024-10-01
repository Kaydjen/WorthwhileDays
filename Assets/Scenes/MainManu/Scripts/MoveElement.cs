using DG.Tweening;
using UnityEngine;

public class MoveElement : MonoBehaviour
{
    [SerializeField] private bool _doMoveAtStart;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _startAfter = 5f;
    [SerializeField] private Data_Text _textsData;
    [SerializeField] private TextAppearAnimation _textAppearAnimation;

    void Start() 
    {
        if(_doMoveAtStart) Invoke(nameof(SmooseMoveStart), _startAfter);
    } 

    private void SmooseMoveStart()
    {
        transform.DOMove(_targetPosition.position, _duration);
        if (_textsData != null)
            StartCoroutine(_textAppearAnimation.TextAnimation(_textsData));
    }
}
