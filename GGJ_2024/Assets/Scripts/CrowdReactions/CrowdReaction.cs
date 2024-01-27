using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crowd Reaction", menuName = "Crowd Stuff/Crowd Reaction")]
public class CrowdReaction : ScriptableObject
{
    public Sprite hair;
    public Sprite emotion;
    public Sprite bodySprite;
    
    public Color skinColor = Color.white;
}
