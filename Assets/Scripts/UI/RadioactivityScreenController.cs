using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioactivityScreenController : MonoBehaviour
{
    [SerializeField]
    private Resource radioactivity;
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

    private bool isWarningOn = false;
    private float counter = 0;
    private bool warningSwitch = false;

    private void Start()
    {
        radioactivity.ValueChanged += OnRadioactivityChanged;
    }

    private void OnRadioactivityChanged(Resource obj)
    {
        float t = obj.Value;

        float radioactivityValue = valueRange.Lerp(t / 100);
        text.text = $"{radioactivityValue,4:000}";

        isWarningOn = !disableWarningRange.IsBetween(t);

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
