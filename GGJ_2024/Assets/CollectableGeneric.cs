using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CollectableGeneric : MonoBehaviour
{
    [SerializeField] 
    private SpriteRandomizer _spriteRandomizer;
    
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = _spriteRandomizer.GetRandomSprite();
    }

}
