using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; //F�r att f� tillg�ng till volymer
using UnityEngine.Rendering.Universal; //F�r att f� tillg�ng till postprocess effekter

public class PoisonEffect : MonoBehaviour
{
    private Volume v;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;
    public float oscillationSpeed = 2f;
    private float x = 0;
    public float hueSpeed;

    private bool poisioned = false;

    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out chromaticAberration);
        v.profile.TryGet(out colorAdjustments);
    }

    void Update()
    {
        if (poisioned)
        {
            float oscillatingValue = Mathf.Sin(Time.time * oscillationSpeed) * 0.5f + 0.5f;
            chromaticAberration.intensity.value = oscillatingValue;
            colorAdjustments.postExposure.value = oscillatingValue;

            if (x <= 180)
            {
                colorAdjustments.hueShift.value = x;
                x += Time.deltaTime * hueSpeed;
            }
            else
            { x = 0; }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        { 
            poisioned = true;
            Debug.Log("Player is poisoned!");
            //Destroy(gameObject);
        }
    }
}
