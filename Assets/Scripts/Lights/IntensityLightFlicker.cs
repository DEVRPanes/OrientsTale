using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class IntensityLightFlicker : MonoBehaviour
{
    public Light2D primeLight;
    [SerializeField] float startTime;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    [SerializeField] float flickerCooldown;


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(startScene());
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(enableLightFlicker());
    }

    IEnumerator enableLightFlicker()
    {
        yield return new WaitForSeconds(flickerCooldown);
        primeLight.intensity = Random.Range(minIntensity, maxIntensity);
        StartCoroutine(enableLightFlicker());
    }
}
