// BGMController.cs
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public MapMove mapMove;
    public float startOffset = 21f;

    void Start()
    {
        mapMove.transform.position += Vector3.left * startOffset;

        float startTime = startOffset / mapMove.speed;
        bgmAudioSource.time = startTime;
        bgmAudioSource.Play();
    }
}
