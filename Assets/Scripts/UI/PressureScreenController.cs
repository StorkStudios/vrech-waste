using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressureScreenController : MonoBehaviour
{
    [SerializeField]
    private Resource pressure;
    [SerializeField]
    private UIRoationController arrow;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private RangeBoundariesFloat valueRange;
    [MinMaxRange(0, 100)]
    [SerializeField]
    private RangeBoundariesFloat disableWarningRange;
    [SerializeField]
    private float blinkDuration;
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private AudioSource audio;

    private bool isWarningOn = false;
    private float counter = 0;
    private bool warningSwitch = false;

    private void Start()
    {
        pressure.ValueChanged += OnPressureChanged;
    }

    private void OnPressureChanged(Resource obj)
    {
        float t = obj.Value;

        float pressureValue = valueRange.Lerp(t / 100);
        text.text = $"{pressureValue:000}";

        bool wasWarningOn = isWarningOn;
        isWarningOn = !disableWarningRange.IsBetween(t);

        if (wasWarningOn != isWarningOn && audio != null)
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

        arrow.ChangeValue(t, 100);
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
