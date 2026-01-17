using UnityEngine;

public class HUDSizeFitter : MonoBehaviour
{

  public Camera vrCamera;
  public float distance = 2.0f; // Distancia en metros (confort VR)

  [Range(0.1f, 1.0f)]
  public float fillPercent = 0.8f; // Qué tanto de la pantalla quieres cubrir (80%)

  void Start()
  {
    if (vrCamera == null)
      vrCamera = Camera.main;

    AdjustHUD();
  }

  void AdjustHUD()
  {
    // 1. Ponemos el Canvas hijo de la cámara si no lo es
    transform.SetParent(vrCamera.transform);

    // 2. Lo colocamos a la distancia correcta
    transform.localPosition = new Vector3(0, 0, distance);
    transform.localRotation = Quaternion.identity; // Recto frente a la cara

    // 3. Calculamos la altura del Frustum a esa distancia
    // (Trigonometría: tan(angulo) = opuesto / adyacente)
    float frustumHeight = 2.0f * distance * Mathf.Tan(vrCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    float frustumWidth = frustumHeight * vrCamera.aspect;

    // 4. Ajustamos el tamaño del RectTransform para que coincida con ese tamaño físico
    RectTransform rect = GetComponent<RectTransform>();

    // Asumimos que la escala del canvas es fija (ej: 0.001)
    float currentScale = transform.localScale.y;

    // Convertimos metros a "píxeles de canvas"
    float targetHeightPixels = frustumHeight / currentScale * fillPercent;
    float targetWidthPixels = frustumWidth / currentScale * fillPercent;

    rect.sizeDelta = new Vector2(targetWidthPixels, targetHeightPixels);
  }
}