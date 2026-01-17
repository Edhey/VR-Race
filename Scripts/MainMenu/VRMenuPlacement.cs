using System.Collections;
using UnityEngine;

public class VRMenuPlacement : MonoBehaviour {

  [Tooltip("¿Quieres que el menú mire al jugador al inicio?")]
  public bool LookAtCamera = true;

  [Tooltip("Ajuste de altura extra (por si queda muy bajo)")]
  public float HeightOffset = 0.0f;

  private IEnumerator Start() {
    yield return new WaitForEndOfFrame();

    transform.SetParent(null);

    if (LookAtCamera) {
      Vector3 cameraPos = Camera.main.transform.position;

      transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                       Camera.main.transform.rotation * Vector3.up);

    }

    transform.position = new Vector3(transform.position.x, transform.position.y + HeightOffset, transform.position.z);
  }
}