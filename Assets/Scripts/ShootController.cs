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

    [SerializeField] private AudioSource shootNoise;
    [SerializeField] private AudioClip shootClip;

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
            if (Hud.Instance != null && Hud.Instance.ammoCount > 0)
            {
                Fire();
                lastFireTime = Time.time;
                Hud.Instance.ammoCount--;
            }
            else if (Hud.Instance == null)
            {
                Fire();
                lastFireTime = Time.time;
            }
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

        if (shootNoise != null)
        {
            if (shootClip != null)
            {
                shootNoise.PlayOneShot(shootClip);
            }
            else if (shootNoise.clip != null)
            {
                shootNoise.PlayOneShot(shootNoise.clip);
            }
            else
            {
                Debug.LogWarning("ShootController: no AudioClip assigned for PlayOneShot.");
            }
        }

        ball.Launch(spawnPoint, direction, shootForce);

    }
}