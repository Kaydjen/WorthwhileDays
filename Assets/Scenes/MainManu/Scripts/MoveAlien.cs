using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlien : MonoBehaviour
{
    [SerializeField] private Vector3 _targetScale = new Vector3(.8f, .8f, .8f);
    [SerializeField] private Vector3 _targetRotation = new Vector3(0f, 25f, 0f);
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _whenToStartScale = 1f;

    public void SmooseChange()
    {
        Invoke(nameof(ChangeScale_Rotation), _whenToStartScale);
    }
    private void ChangeScale_Rotation()
    {
        transform.DOScale(_targetScale, _duration);
        transform.DORotate(_targetRotation, _duration, RotateMode.FastBeyond360);
    }
}
