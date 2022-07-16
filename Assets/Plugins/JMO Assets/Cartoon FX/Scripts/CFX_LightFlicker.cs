using UnityEngine;
using System.Collections;

// Cartoon FX  - (c) 2015 Jean Moreno

// Randomly changes a light's intensity over time.

[ExecuteAlways]
[RequireComponent(typeof(Light))]
public class CFX_LightFlicker : MonoBehaviour
{
    // Loop flicker effect
    public bool loop;

    // Perlin scale: makes the flicker more or less smooth
    public float smoothFactor = 1f;

    /// Max intensity will be: baseIntensity + addIntensity
    public float addIntensity = 1.0f;

    public float baseIntensity = 1f;

    private float minIntensity;
	private float maxIntensity;
	
	void Update ()
	{
        minIntensity = baseIntensity;
        maxIntensity = baseIntensity + addIntensity;
        GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * smoothFactor,  0f));
	}
}
