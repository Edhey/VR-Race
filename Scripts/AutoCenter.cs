using UnityEngine;
using System.Collections;

public class AutoCenter : MonoBehaviour {
  public Transform target;
  public Transform vrCamera;

  private void Start() {
    StartCoroutine(CenterView());
  }

  private IEnumerator CenterView() {
    yield return _waitForSeconds0_1;
    if (target != null && vrCamera != null) {
      float userRotationY = vrCamera.localEulerAngles.y;
      Vector3 directionToCar = target.position - transform.position;
      directionToCar.y = 0;
      Quaternion rotationTowardsCar = Quaternion.LookRotation(directionToCar);
      float finalAngle = rotationTowardsCar.eulerAngles.y - userRotationY;
      transform.rotation = Quaternion.Euler(0, finalAngle, 0);
    }
  }
  private static readonly WaitForSeconds _waitForSeconds0_1 = new(0.1f);
}