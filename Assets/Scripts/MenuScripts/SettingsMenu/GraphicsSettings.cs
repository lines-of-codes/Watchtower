using CommandTerminal;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The graphics page settings
/// </summary>
public partial class GraphicsSettings : MonoBehaviour
{
    [SerializeField] UniversalAdditionalCameraData camData;
    [SerializeField] VolumeProfile globalVolume;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle vsyncToggle;

    static GraphicsSettings s_instance;
    static bool s_renderPostProcessing = true;
    static bool s_enableTonemapping = true;
    static bool s_enableColorAdjustments = true;
    static bool s_enableVignette = true;
    static bool s_enableBloom = true;
    Bloom bloom;
    Tonemapping tonemapping;
    ColorAdjustments colorAdjustments;
    Vignette vignette;

    private void Awake()
    {
        s_instance = this;

        if (camData == null) Camera.main.GetComponent<UniversalAdditionalCameraData>();

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        vsyncToggle.isOn = QualitySettings.vSyncCount != 0;

        camData.renderPostProcessing = s_renderPostProcessing;
        if (globalVolume.TryGet(out bloom))
            bloom.active = s_enableBloom;
        else
            bloom = globalVolume.Add<Bloom>(true);

        if (globalVolume.TryGet(out tonemapping))
            tonemapping.active = s_enableTonemapping;
        else
            tonemapping = globalVolume.Add<Tonemapping>(true);

        if (globalVolume.TryGet(out colorAdjustments))
            colorAdjustments.active = s_enableColorAdjustments;
        else
            colorAdjustments = globalVolume.Add<ColorAdjustments>(true);

        if (globalVolume.TryGet(out vignette))
            vignette.active = s_enableVignette;
        else
            vignette = globalVolume.Add<Vignette>(true);
    }

    public void SetQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
    }

    public void EnableVSync(bool enable) => QualitySettings.vSyncCount = enable ? 1 : 0;

    [RegisterCommand("Quality.VSync", MinArgCount = 1, MaxArgCount = 1, Help = "Enable or disable VSync. (bool)")]
    public static void CommandVSync(CommandArg[] args) => s_instance.EnableVSync(args[0].Bool);

    public void EnablePostProcessing(bool enable)
    {
        s_renderPostProcessing = enable;
        camData.renderPostProcessing = enable;
    }

    // Bloom

    public void EnableBloom(bool enable)
    {
        s_enableBloom = enable;
        bloom.active = enable;
    }

    public void SetBloomThreshold(float value) => bloom.threshold.value = value;
    public void SetBloomIntensity(float value) => bloom.intensity.value = value;

    [RegisterCommand("Bloom.Threshold", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Bloom > Threshold property. (float)")]
    public static void CommandSetBloomThreshold(CommandArg[] args) => s_instance.SetBloomThreshold(args[0].Float);
    [RegisterCommand("Bloom.Intensity", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Bloom > Intensity property. (float)")]
    public static void CommandSetBloomIntensity(CommandArg[] args) => s_instance.SetBloomThreshold(args[0].Float);

    // Tonemapping

    public void EnableTonemapping(bool enable)
    {
        s_enableTonemapping = enable;
        tonemapping.active = enable;
    }

    public void SetTonemapping(int value)
    {
        tonemapping.mode.value = value switch
        {
            0 => TonemappingMode.None,
            1 => TonemappingMode.Neutral,
            2 => TonemappingMode.ACES,
            _ => throw new NotImplementedException()
        };
    }

    // Color Adjustments
    public void EnableColorAdjustments(bool enable)
    {
        s_enableColorAdjustments = enable;
        colorAdjustments.active = enable;
    }

    public void SetPostExposure(float intensity) => colorAdjustments.postExposure.value = intensity;
    public void SetContrast(float contrast) => colorAdjustments.contrast.value = contrast;
    public void SetColorFilter(Color color) => colorAdjustments.colorFilter.value = color;
    public void SetSaturation(float saturation) => colorAdjustments.saturation.value = saturation;
    public void SetHueShift(float hueShift) => colorAdjustments.hueShift.value = hueShift;

    [RegisterCommand("CA.HueShift", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Color Adjustments > Hue Shift property. (float)")]
    public static void CommandHueShift(CommandArg[] args) => s_instance.SetHueShift(args[0].Float);
    [RegisterCommand("CA.Saturation", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Color Adjustments > Saturation property. (float)")]
    public static void CommandSetSaturation(CommandArg[] args) => s_instance.SetSaturation(args[0].Float);
    [RegisterCommand("CA.ColorFilter", MinArgCount = 3, MaxArgCount = 3, Help = "Set the Color Adjustments > Color Filter property. (float, float, float)")]
    public static void CommandColorFilter(CommandArg[] args) => s_instance.SetColorFilter(new Color(args[0].Float, args[1].Float, args[2].Float));
    [RegisterCommand("CA.Contrast", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Color Adjustments > Contrast property. (float)")]
    public static void CommandSetContrast(CommandArg[] args) => s_instance.SetContrast(args[0].Float);
    [RegisterCommand("CA.PostExposure", MinArgCount = 1, MaxArgCount = 1, Help = "Set the Color Adjustments > Post Exposure property. (float)")]
    public static void CommandSetPostExposure(CommandArg[] args) => s_instance.SetPostExposure(args[0].Float);

    // Vignette
    public void EnableVignette(bool enable)
    {
        s_enableVignette = enable;
        vignette.active = enable;
    }

    public void SetVignetteColor(Color color) => vignette.color.value = color;
    public void SetVignetteIntensity(float intensity) => vignette.intensity.value = intensity;
    public void SetVignetteSmoothness(float smoothness) => vignette.smoothness.value = smoothness;
    public void IsVignetteRound(bool isRound) => vignette.rounded.value = isRound;
}
