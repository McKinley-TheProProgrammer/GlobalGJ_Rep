using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class UIGenericMove : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransformToMove;
    private Vector2 startPos;

    [SerializeField]
    private Vector2Reference moveDistance;
    
    [SerializeField] 
    private float duration = .15f;

    [SerializeField] 
    private float waitDuration = 1f;
    
    
    private Sequence moveSequence = null;
    private Sequence moveBack = null;

    void Start()
    {
        if (!rectTransformToMove)
            rectTransformToMove = GetComponent<RectTransform>();
        
        startPos = rectTransformToMove.position;
    }

    public void Move()
    {
        if(moveSequence != null && moveSequence.IsActive())
            return;
        
        if(moveBack != null && moveBack.IsActive())
            return;
        
        moveSequence = DOTween.Sequence().SetId($"GO: {gameObject.name}");

        moveSequence.Append(rectTransformToMove.DOMove(moveDistance.Value, duration).From(startPos))
            .SetUpdate(isIndependentUpdate:true);
        
        moveSequence.Play().SetUpdate(true);
    }
    
    public void Move(Action endCallback)
    {
        if(moveSequence != null && moveSequence.IsActive())
            return;
        
        if(moveBack != null && moveBack.IsActive())
            return;
        
        moveSequence = DOTween.Sequence().SetId($"GO: {gameObject.name}");

        moveSequence.Append(rectTransformToMove.DOMove(moveDistance.Value, duration).From(startPos))
            .SetUpdate(isIndependentUpdate:true);

        moveSequence.AppendCallback(() =>
        {
            endCallback?.Invoke();
        });

        moveSequence.Play().SetUpdate(true);
    }

    public void MoveBack()
    {
        if(moveSequence != null && moveSequence.IsActive())
            return;
        
        if(moveBack != null && moveBack.IsActive())
            return;
        
        moveBack = DOTween.Sequence().SetId($"GO: {gameObject.name}");

        moveBack.Append(rectTransformToMove.DOMove(startPos, duration).From(moveDistance.Value))
            .SetUpdate(isIndependentUpdate:true);
        
        moveBack.Play().SetUpdate(true);
    }
    
    
    public void MoveBack(Action endCallback)
    {
        if(moveSequence != null && moveSequence.IsActive())
            return;
        
        if(moveBack != null && moveBack.IsActive())
            return;
        
        moveBack = DOTween.Sequence().SetId($"GO: {gameObject.name}");

        moveBack.Append(rectTransformToMove.DOMove(startPos, duration).From(moveDistance.Value))
            .SetUpdate(isIndependentUpdate:true);

        moveBack.AppendCallback(() =>
        {
            endCallback?.Invoke();
        });

        moveBack.Play().SetUpdate(true);
    }
    
}
