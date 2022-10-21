using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [SerializeField] private float volume = 0.01f;

    private AudioSource audioSource;

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
        audioSource.Play();
    }
}
