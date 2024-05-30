using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRLever : XRBaseInteractable
{
    [SerializeField]
    [Tooltip("The object that is visually grabbed and manipulated")]
    Transform m_Handle = null;

    [SerializeField]
    [Tooltip("The value of the lever")]
    float m_Value = 0;

    [SerializeField]
    [Range(0.0f, 90.0f)]
    float m_MaxAngle = 90.0f;

    [SerializeField]
    [Tooltip("Events to trigger when the lever activates")]
    UnityEvent<float> m_OnLeverValueChange = new UnityEvent<float>();

    IXRSelectInteractor m_Interactor;

    /// <summary>
    /// The object that is visually grabbed and manipulated
    /// </summary>
    public Transform handle
    {
        get => m_Handle;
        set => m_Handle = value;
    }

    /// <summary>
    /// The value of the lever
    /// </summary>
    public float value
    {
        get => m_Value;
        set => SetValue(value);
    }

    /// <summary>
    /// If enabled, the lever will snap to the value position when released
    /// </summary>
    public bool lockToValue { get; set; }

    /// <summary>
    /// Angle of the lever in the 'on' position
    /// </summary>
    public float maxAngle
    {
        get => m_MaxAngle;
        set => m_MaxAngle = value;
    }

    void Start()
    {
        SetValue(m_Value);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartGrab);
        selectExited.AddListener(EndGrab);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(StartGrab);
        selectExited.RemoveListener(EndGrab);
        base.OnDisable();
    }

    void StartGrab(SelectEnterEventArgs args)
    {
        m_Interactor = args.interactorObject;
    }

    void EndGrab(SelectExitEventArgs args)
    {
        SetValue(m_Value);
        m_Interactor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                UpdateValue();
            }
        }
    }

    Vector3 GetLookDirection()
    {
        Vector3 direction = m_Interactor.GetAttachTransform(this).position - m_Handle.position;
        direction = transform.InverseTransformDirection(direction);
        direction.x = 0;

        return direction.normalized;
    }

    void UpdateValue()
    {
        var lookDirection = GetLookDirection();
        var lookAngle = Mathf.Atan2(lookDirection.z, lookDirection.y) * Mathf.Rad2Deg;

        lookAngle = Mathf.Clamp(lookAngle, -maxAngle, maxAngle);

        SetHandleAngle(lookAngle);

        SetValue(lookAngle / (2 * maxAngle) + 0.5f);
    }

    void SetValue(float value)
    {
        m_Value = value;

        m_OnLeverValueChange?.Invoke(m_Value);
    }

    void SetHandleAngle(float angle)
    {
        if (m_Handle != null)
            m_Handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    }

    void OnDrawGizmosSelected()
    {
        var angleStartPoint = transform.position;

        if (m_Handle != null)
            angleStartPoint = m_Handle.position;

        const float k_AngleLength = 0.25f;

        var angleMaxPoint = angleStartPoint + transform.TransformDirection(Quaternion.Euler(m_MaxAngle, 0.0f, 0.0f) * Vector3.up) * k_AngleLength;
        var angleMinPoint = angleStartPoint + transform.TransformDirection(Quaternion.Euler(-m_MaxAngle, 0.0f, 0.0f) * Vector3.up) * k_AngleLength;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(angleStartPoint, angleMaxPoint);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(angleStartPoint, angleMinPoint);
    }

    void OnValidate()
    {
        SetHandleAngle(m_Value);
    }
}
