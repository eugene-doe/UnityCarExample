using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class Dashboard : MonoBehaviour
{
    public CarController carController;
    public Transform speedometerNeedle;
    public Transform tachometerNeedle;
    public Transform steeringWheel;

    [Range(0, 360)]
    public float maxAngle = 270f;
    [Range(1f, 10f)]
    public float steeringMultiplier = 8f;
    public bool MilesToKm;

    private const float milesToKm = 3.6f / 2.23693629f; // Magic numbers from CarController

    void Update()
    {
        float relativeSpeed = carController.CurrentSpeed / carController.MaxSpeed;
        float indicatedSpeed = relativeSpeed * maxAngle;
        if (MilesToKm) { indicatedSpeed *= milesToKm; }

        Vector3 speed = speedometerNeedle.localRotation.eulerAngles;
        speed.z = -indicatedSpeed;
        speedometerNeedle.localRotation = Quaternion.Euler(speed);

        Vector3 revs = tachometerNeedle.localRotation.eulerAngles;
        revs.z = -carController.Revs * maxAngle;
        tachometerNeedle.localRotation = Quaternion.Euler(revs);

        Vector3 steering = steeringWheel.localRotation.eulerAngles;
        steering.z = -carController.CurrentSteerAngle * steeringMultiplier;
        steeringWheel.localRotation = Quaternion.Euler(steering);
    }
}
