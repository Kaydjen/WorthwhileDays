using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElementsAnimations : MonoBehaviour
{
    [SerializeField] private List<Data_MoveElements> _data = new List<Data_MoveElements>();

    private IEnumerator ElementsMoveAnimation(Data_Elements element)
    {
        yield return new WaitForSeconds(element.WhenToStart);
        element.ObjToMove.DOMove(element.TargetPosition.position, element.Duration);
    }

    public void StartMoveObjects(int index)
    {
        if (index >= _data.Count) return; // Ensure index is within bounds of _data
        if (_data[index].Elements.Count == 0) return; // Check if there are elements to move

        for (int i = 0; i < _data[index].Elements.Count; i++)
        {
            var element = _data[index].Elements[i];
            StartCoroutine(ElementsMoveAnimation(element));
        }
    }
}

// TODO: зробити через структуру
[System.Serializable]
public class Data_MoveElements
{
    public List<Data_Elements> Elements = new List<Data_Elements>();
}

[System.Serializable]
public class Data_Elements
{
    public Transform ObjToMove;
    public Transform TargetPosition;
    public float Duration = 0.05f;
    public float WhenToStart = 1f;
}

/*
 
 using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElementsAnimations : MonoBehaviour
{
    [SerializeField] private List<Data_MoveElements> _data = new List<Data_MoveElements>();

    private IEnumerator ElementsMoveAnimation(Data_Elements data)
    {
        yield return new WaitForSeconds(data.WhenToStart);
        data.ObjToMove.DOMove(data.TargetPosition.position, data.Duration);
    }
    public void StartMoveObjects(int index)
    {
        if (index >= _data.Count) return;
        if (index >= _data[index].Elements.Count) return;

        for (int i = 0; i < _data[index].Elements.Count; i++)
            StartCoroutine(ElementsMoveAnimation(_data[index].Elements[i]));
    }
}


[System.Serializable]
public class Data_MoveElements
{
    public List<Data_Elements> Elements = new List<Data_Elements>();
}


[System.Serializable]
public class Data_Elements
{
    public Transform ObjToMove;
    public Transform TargetPosition;
    public float Duration = .05f;
    public float WhenToStart = 1f;
}

 
 */