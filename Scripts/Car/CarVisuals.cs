using UnityEngine;

public class CarVisuals : MonoBehaviour {
  [Header("References")]
  [SerializeField] private CarController _carController;
  [SerializeField] private WheelCollider[] _colliders;
  [SerializeField] private Transform[] _wheelMeshes; // Deben coincidir en orden con los colliders

  [Header("Effects")]
  [SerializeField] private ParticleSystem[] _dustParticles;
  [SerializeField] private TrailRenderer[] _trails;

  private void Update() {
    UpdateWheelPoses();
    UpdateEffects();
  }

  private void UpdateWheelPoses() {
    for (int i = 0; i < _colliders.Length; i++) {
      _colliders[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
      _wheelMeshes[i].SetPositionAndRotation(pos, rot);
    }
  }

  private void UpdateEffects() {
    bool shouldEmit = _carController.IsEmittingParticles();

    foreach (ParticleSystem ps in _dustParticles) {
      if (shouldEmit && !ps.isEmitting) {
        ps.Play();
      } else if (!shouldEmit && ps.isEmitting) {
        ps.Stop();
      }
    }

    foreach (TrailRenderer tr in _trails) {
      tr.emitting = shouldEmit;
    }
  }
}
