using UnityEngine;

[System.Serializable]
public class AudioElement
{
    public string clipName = "";
    public AudioClip clip = null;
    [Range(0f, 1f)] public float volume = 1;
    public float pitch = 1;

    [HideInInspector] public AudioSource source = null;
}
