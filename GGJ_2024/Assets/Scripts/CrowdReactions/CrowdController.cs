using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdController : MonoBehaviour
{
    [SerializeField] 
    private CrowdReaction crowdReaction;

    [SerializeField] 
    private Image hairIcon, headIcon, expressionIcon, bodyIcon;
    
    public void SetCrowdReaction(CrowdReaction crowdReaction)
    {
        this.crowdReaction = crowdReaction;
        
        hairIcon.sprite = crowdReaction.hair;
        headIcon.color = crowdReaction.skinColor;
        bodyIcon.sprite = crowdReaction.bodySprite;
        
        expressionIcon.sprite = crowdReaction.emotion;
    }
    


    private void Start()
    {
        SetCrowdReaction(crowdReaction);
    }
}
