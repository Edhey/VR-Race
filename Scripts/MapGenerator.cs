using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MapGenerator : MonoBehaviour {

    [Header("Referencia")]
    public Transform checkpointsParent;

    [Header("Ajustes Visuales")]
    public float height = 50f;

    void Start() {
        DrawMap();
    }

    void DrawMap() {
        LineRenderer line = GetComponent<LineRenderer>();

        // 1. Contar cuántos hijos (checkpoints) hay
        int pointCount = checkpointsParent.childCount;
        line.positionCount = pointCount;

        // 2. Recorrerlos y guardar sus posiciones
        for (int i = 0; i < pointCount; i++) {
            Transform checkpoint = checkpointsParent.GetChild(i);
            Vector3 pos = checkpoint.position;
            pos.y = height;
            line.SetPosition(i, pos);
        }
    }
}