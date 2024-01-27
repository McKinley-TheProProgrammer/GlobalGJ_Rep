using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class UIGenericPop : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransformToPop;
    private Vector2 startScale;

    [SerializeField] 
    private FloatReference popFactor;
    [SerializeField] 
    private float popDuration = .5f;

    private Sequence popInSequence = null;
    private Sequence popOutSequence = null;

    public bool loop;
    
    public bool debugInfo = false;
    private void Start()
    {
        if (!rectTransformToPop)
            rectTransformToPop = GetComponent<RectTransform>();

        startScale = rectTransformToPop.localScale;
    }

    public void PopIn()
    {
        if(popInSequence != null && popInSequence.IsActive())
            return;
        
        if(popOutSequence != null && popOutSequence.IsActive())
            return;
        
        popInSequence = DOTween.Sequence().SetId($"{gameObject.name} PopUp");
        popInSequence.Append(rectTransformToPop.DOScale(Vector2.one * popFactor.Value, popDuration))
            .SetUpdate(isIndependentUpdate: true);

        popInSequence.AppendCallback(() =>
        {
            if(debugInfo)
                Debug.Log($"Starting POP IN sequence. Id: <color=\"cyan\">{popInSequence.id}</color> GO: <color=\"cyan\">{gameObject.name}</color>");
            
        });

        if (loop)
            popInSequence.SetLoops(2, LoopType.Yoyo);
        
        popInSequence.Play().SetUpdate(true);
    }
    
    public void PopIn(Action endCallback)
    {
        if(popInSequence != null && popInSequence.IsActive())
            return;
        
        if(popOutSequence != null && popOutSequence.IsActive())
            return;
        
        popInSequence = DOTween.Sequence().SetId($"{gameObject.name} PopUp");
        popInSequence.Append(rectTransformToPop.DOScale(Vector2.one * popFactor.Value, popDuration))
            .SetUpdate(isIndependentUpdate: true);

        popInSequence.AppendCallback(() =>
        {
            if(debugInfo)
                Debug.Log($"Starting POP IN sequence. Id: <color=\"cyan\">{popInSequence.id}</color> GO: <color=\"cyan\">{gameObject.name}</color>");
            
            endCallback?.Invoke();
        });

        if (loop)
            popInSequence.SetLoops(2, LoopType.Yoyo);
        
        popInSequence.Play().SetUpdate(true);

    }

    public void PopOut()
    {
        if(popInSequence != null && popInSequence.IsActive())
            return;
        
        if(popOutSequence != null && popOutSequence.IsActive())
            return;
        
        popOutSequence = DOTween.Sequence().SetId($"{gameObject.name} PopUp");
        popOutSequence.Append(rectTransformToPop.DOScale(startScale, popDuration))
            .SetUpdate(isIndependentUpdate: true);

        popOutSequence.AppendCallback(() =>
        {
            if(debugInfo)
                Debug.Log($"Starting POP OUT sequence. Id: <color=\"cyan\">{popOutSequence.id}</color> GO: <color=\"cyan\">{gameObject.name}</color>");
            
        });

        if (loop)
            popInSequence.SetLoops(2, LoopType.Yoyo);
        
        popOutSequence.Play().SetUpdate(true);
    }
    
    public void PopOut(Action endCallback)
    {
        if(popInSequence != null && popInSequence.IsActive())
            return;
        
        if(popOutSequence != null && popOutSequence.IsActive())
            return;
        
        popOutSequence = DOTween.Sequence().SetId($"{gameObject.name} PopUp");
        popOutSequence.Append(rectTransformToPop.DOScale(startScale, popDuration))
            .SetUpdate(isIndependentUpdate: true);

        popOutSequence.AppendCallback(() =>
        {
            if(debugInfo)
                Debug.Log($"Starting POP OUT sequence. Id: <color=\"cyan\">{popOutSequence.id}</color> GO: <color=\"cyan\">{gameObject.name}</color>");
            
            endCallback?.Invoke();
        });
        
        if (loop)
            popInSequence.SetLoops(2, LoopType.Yoyo);

        popOutSequence.Play().SetUpdate(true);
    }
        
}
