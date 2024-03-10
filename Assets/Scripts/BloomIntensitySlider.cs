using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class BloomIntensityControl : MonoBehaviour
{
    public Volume globalVolume;
    private Bloom bloom;

    public void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider value changed: " + value);
        bloom.intensity.value = value;
    }

}