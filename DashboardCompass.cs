using UnityEngine;
using UnityEngine.UI;

public class DashboardCompass : MonoBehaviour {
    [Header("Referencias UI")]
    [SerializeField] private RectTransform needleRect;
    [SerializeField] private Transform carTransform;

    [Header("Configuración")]
    [SerializeField] private bool useTrueNorth = true;
    [SerializeField] private float smoothing = 5f;

    void Start() {
        Input.compass.enabled = true;

        if (useTrueNorth) {
            Input.location.Start();
        }
    }

    void Update() {
        float northDirection = useTrueNorth ? Input.compass.trueHeading : Input.compass.magneticHeading;
        float carY = carTransform.eulerAngles.y;
        float targetAngle = northDirection - carY;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        needleRect.localRotation = Quaternion.Slerp(needleRect.localRotation, targetRotation, Time.deltaTime * smoothing);
    }
}