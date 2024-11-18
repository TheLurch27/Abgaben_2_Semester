using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab; // Das Auto-Prefab, das gespawnt werden soll
    public Transform pointA; // Spawnpunkt A
    public Transform pointC; // Spawnpunkt C
    public Transform pointB; // Ziel-Punkt B
    public Transform pointD; // Ziel-Punkt D

    private bool spawnAtPointA = true; // Steuerung, an welchem Punkt das Auto gespawnt wird

    // Diese Methode wird durch den Button im UI aufgerufen
    public void SpawnCar()
    {
        Transform spawnPoint = spawnAtPointA ? pointA : pointC;
        Quaternion spawnRotation = spawnAtPointA ? Quaternion.Euler(0, 270, 0) : spawnPoint.rotation;

        GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnRotation);
        CarStateMachine carStateMachine = car.GetComponent<CarStateMachine>();

        Transform targetPoint = spawnAtPointA ? pointB : pointD;
        carStateMachine.SetTargetPoint(targetPoint); // Ziel setzen
        carStateMachine.SetState(new ApproachState()); // Beginne mit dem ApproachState

        spawnAtPointA = !spawnAtPointA;
    }

}
