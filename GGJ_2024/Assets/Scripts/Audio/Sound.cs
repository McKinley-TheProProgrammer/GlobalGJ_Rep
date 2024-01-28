using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    [HideInInspector]
    public AudioSource AudioSource;
    
    public string tagClip;
    
    public AudioClip clip;

    [Range(0f, 1f)] 
    public float volume;
    
    [Range(0.1f, 3f)] 
    public float pitch;

    public bool loop;

}
