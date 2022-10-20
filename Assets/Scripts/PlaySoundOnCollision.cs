using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private float volume = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.volume = collision.relativeVelocity.magnitude * volume;
        Debug.Log(audioSource.volume);
        audioSource.Play();
    }
}
