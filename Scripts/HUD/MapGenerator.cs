using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class MapGenerator : MonoBehaviour {

  [Header("Referencia")]
  [FormerlySerializedAs("checkpointsParent")]
  [SerializeField] private Transform _checkpointsParent;

  [Header("Ajustes Visuales")]
  [FormerlySerializedAs("height")]
  [SerializeField] private float _height = 50f;

  private void Start() {
    DrawMap();
  }

  private void DrawMap() {
    LineRenderer line = GetComponent<LineRenderer>();

    // 1. Contar cuántos hijos (checkpoints) hay
    int pointCount = _checkpointsParent.childCount;
    line.positionCount = pointCount;

    // 2. Recorrerlos y guardar sus posiciones
    for (int i = 0; i < pointCount; i++) {
      Transform checkpoint = _checkpointsParent.GetChild(i);
      Vector3 pos = checkpoint.position;
      pos.y = _height;
      line.SetPosition(i, pos);
    }
  }
}