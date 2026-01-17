using UnityEngine;

/// <summary>
/// Controla la rotación de la cámara usando el ratón.
/// Esta clase nos permite simular el movimiento de cabeza en VR cuando estamos
/// en el editor de Unity
///  </summary>
public class MouseLook : MonoBehaviour {

    [Header("Configuración")]
    public float Sensitivity = 2.0f; // Velocidad del ratón
    public bool LockCursor = true;
    public float MinX = -90f;
    public float MaxX = 90f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

  private void Start() {
        Vector3 currentRot = transform.localEulerAngles;
        _rotationX = currentRot.x;
        _rotationY = currentRot.y;
        if (LockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

  private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity;
        _rotationY += mouseX; // MouseX rota alrededor del eje Y (Izquierda/Derecha)
        _rotationX -= mouseY; // MouseY rota alrededor del eje X (Arriba/Abajo) lo restamos para que no se invierta

        // Limitamos la mirada vertical para evitar que la cámara de vueltas completas sobre sí misma
        _rotationX = Mathf.Clamp(_rotationX, MinX, MaxX);

        // Aplicamos la rotación a la cámara
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0f);

        if (Input.GetKeyDown(KeyCode.Escape)) { // Liberar el cursor con ESC
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
#endif
    }
}