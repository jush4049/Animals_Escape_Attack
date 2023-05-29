using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BulletFire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Bullet;
    public Transform FirePos;

    [SerializeField] private float rotationLimit;
    [SerializeField] private float rotationSpeed;

    private bool rotate = false;

    public AudioClip PlayerfireSfx;
    private AudioSource PlayerAudioSource = null;

    void Start()
    {
        PlayerAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        float targetRotate = rotate ? rotationLimit : 0f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(targetRotate, 0, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        PlayerAudioSource.PlayOneShot(PlayerfireSfx);
        rotate = true;
        Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        rotate = false;
    }
}
