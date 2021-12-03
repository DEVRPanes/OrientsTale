using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RangeLightFlicker : MonoBehaviour
{
    public Light2D primeLight;
    [SerializeField] float startTime;
    [SerializeField] float minOuterRange;
    [SerializeField] float maxOuterRange;
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
        primeLight.pointLightOuterRadius = Random.Range(minOuterRange, maxOuterRange);
        StartCoroutine(enableLightFlicker());
    }
}
