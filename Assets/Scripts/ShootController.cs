using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunController gunController;
    [SerializeField] private Transform barrelTip; // optional — leave empty to use this transform

    [Header("Firing")]
    [SerializeField] private float shootForce = 12f;
    [SerializeField] private float fireRate = 0.5f;  // minimum seconds between shots

    private float lastFireTime = -999f;
    private Mouse mouse;
    private Keyboard keyboard;

    void Start()
    {
        mouse = Mouse.current;
        keyboard = Keyboard.current;
    }

    void Update()
    {
        bool firePressed = false;

        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            firePressed = true;

        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
            firePressed = true;

        if (firePressed && Time.time >= lastFireTime + fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        Vector3 spawnPoint = barrelTip != null ? barrelTip.position : transform.position;
        Vector3 direction = gunController.GetBarrelDirection();

        GameObject ballGO = MartyPool.Instance.Get();
        MartySupreme ball = ballGO.GetComponent<MartySupreme>();

        // Place it at the barrel before physics kicks in
        ballGO.transform.position = spawnPoint;

        ball.Launch(spawnPoint, direction, shootForce);
    }
}