using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoCenter : MonoBehaviour {
  [FormerlySerializedAs("target")]
  [SerializeField] private Transform _target;
  [FormerlySerializedAs("vrCamera")]
  [SerializeField] private Transform _vrCamera;

  private void Start() {
    StartCoroutine(CenterView());
  }

  private IEnumerator CenterView() {
    yield return _waitForSeconds0_1;
    if (_target != null && _vrCamera != null) {
      float userRotationY = _vrCamera.localEulerAngles.y;
      Vector3 directionToCar = _target.position - transform.position;
      directionToCar.y = 0;
      Quaternion rotationTowardsCar = Quaternion.LookRotation(directionToCar);
      float finalAngle = rotationTowardsCar.eulerAngles.y - userRotationY;
      transform.rotation = Quaternion.Euler(0, finalAngle, 0);
    }
  }
  private static readonly WaitForSeconds _waitForSeconds0_1 = new(0.1f);
}