using UnityEngine;

public class CarLoader : MonoBehaviour {
  [Header("Current In-Scene Car")]
  public GameObject CurrentCar;

  private void Start() {
    GameObject carToSpawn = GameManager.Instance.SelectedCarPrefab;
    if (carToSpawn != null && carToSpawn != CurrentCar) {
      CurrentCar.transform.GetPositionAndRotation(out Vector3 spawnPosition, out Quaternion spawnRotation);
      GameObject newCar = Instantiate(carToSpawn, spawnPosition, spawnRotation);
      Destroy(CurrentCar);
      CurrentCar = newCar;
    }
  }
}