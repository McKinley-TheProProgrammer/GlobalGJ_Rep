using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGenericFade : MonoBehaviour
{

    [SerializeField]
    private Image _image;
    
    private Sequence fadeInSequence = null;
    private Sequence fadeOutSequence = null;


    public void FadeIn(Action endCallback)
    {
        
    }

    public void FadeOut(Action endCallback)
    {
        
    }
}
