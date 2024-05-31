using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThermometerScreenController : MonoBehaviour
{
    [SerializeField]
    private Resource temperatureResource;
    [SerializeField]
    private UIBarController bar;
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private TextMeshProUGUI temperatureText;
    [MinMaxRange(0, 100)]
    [SerializeField]
    private RangeBoundariesFloat disableWarningRange;
    [SerializeField]
    private float blinkDuration;
    [SerializeField]
    private RangeBoundariesFloat temperatureRange;
    [SerializeField]
    private AudioSource audio;

    private bool isWarningOn = false;
    private float counter = 0;
    private bool warningSwitch = false;

    private void Start()
    {
        temperatureResource.ValueChanged += OnTemperatureChanged;
    }

    private void OnDestroy()
    {
        temperatureResource.ValueChanged -= OnTemperatureChanged;
    }

    private void OnTemperatureChanged(Resource obj)
    {
        float t = obj.Value;

        float temperatureValue = temperatureRange.Lerp(t / 100);
        temperatureText.text = $"{temperatureValue:000}°C";

        bool wasWarningOn = isWarningOn;
        isWarningOn = !disableWarningRange.IsBetween(t);

        if (wasWarningOn != isWarningOn)
        {
            if (isWarningOn)
            {
                audio.Play();
            }
            else
            {
                audio.Stop();
            }
        }

        bar.ChangeValue(t, 100);
    }

    private void Update()
    {
        if ((counter += Time.deltaTime) >= blinkDuration)
        {
            counter = 0;
            warningSwitch = !warningSwitch;
            warning.SetActive(warningSwitch && isWarningOn);
        }
    }
}
