using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab; // Auto Prefab
    [SerializeField] private Transform spawnPointA; // Spawnpoint A
    [SerializeField] private Transform spawnPointC; // Spawnpoint C
    [SerializeField] private Transform despawnPointB; // Despawnpoint B
    [SerializeField] private Transform despawnPointD; // Despawnpoint D

    private bool spawnAtA = true; // Wechsel zwischen Spawnpoint A und C

    public void SpawnCar()
    {
        // Bestimme den Spawn- und Despawnpunkt
        Transform spawnPoint = spawnAtA ? spawnPointA : spawnPointC;
        Transform targetPoint = spawnAtA ? despawnPointB : despawnPointD;

        // Spawne das Auto
        GameObject newCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

        // Weise dem Auto den Zielpunkt zu
        CarController carController = newCar.GetComponent<CarController>();
        if (carController != null)
        {
            carController.SetTarget(targetPoint);
        }
        else
        {
            Debug.LogError("CarController not found on spawned car!");
        }

        // Wechsel den Spawnpunkt für das nächste Auto
        spawnAtA = !spawnAtA;
    }
}
