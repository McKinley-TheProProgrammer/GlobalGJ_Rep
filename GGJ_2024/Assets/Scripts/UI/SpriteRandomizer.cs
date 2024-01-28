using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpriteRandomizer : ScriptableObject
{
    [SerializeField] 
    private List<Sprite> sprites;
    
    public Sprite GetRandomSprite()
    {
        if (sprites.Count == 0)
        {
            Debug.LogError($"No Sprites founds");
            return null;
        }

        return sprites[Random.Range(0, sprites.Count)];
    }
}
