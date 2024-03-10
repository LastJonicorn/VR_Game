using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BloomControl : MonoBehaviour
{
    public Slider bloomSlider;
    public PostProcessVolume postProcessVolume;
    Bloom bloomLayer;

    void Start()
    {
        if (postProcessVolume.profile.TryGetSettings(out bloomLayer))
        {
            // Initialize the slider value to the current bloom intensity
            bloomSlider.value = bloomLayer.intensity.value;
        }
        else
        {
            Debug.LogError("Bloom effect not found in the Post-Processing Volume.");
        }
    }

    public void OnBloomIntensityChanged(float value)
    {
        // Update bloom intensity when the slider value changes
        bloomLayer.intensity.value = value;
    }
}
