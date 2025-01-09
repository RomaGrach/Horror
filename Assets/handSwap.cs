using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class handSwap : MonoBehaviour
{
    public List<GameObject> weapons;
    public Transform hiddenPositionObject;
    public float switchSpeed = 5f;

    private int currentWeaponIndex = -1;
    private bool isSwitching = false;
    private List<Vector3> initialPositions = new List<Vector3>();

    void Start()
    {
        // Запоминаем исходные позиции
        foreach (GameObject weapon in weapons)
        {
            initialPositions.Add(weapon.transform.localPosition);
            weapon.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSwitching)
        {
            StartCoroutine(SwitchWeapon());
        }
    }

    private IEnumerator SwitchWeapon()
    {
        isSwitching = true;

        // Disable current weapon
        if (currentWeaponIndex >= 0)
        {
            yield return MoveWeapon(weapons[currentWeaponIndex], hiddenPositionObject.localPosition);
            weapons[currentWeaponIndex].SetActive(false);
        }

        // Increment weapon index
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Enable next weapon
        if (currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].transform.position = hiddenPositionObject.position;
            weapons[currentWeaponIndex].SetActive(true);
            yield return MoveWeapon(weapons[currentWeaponIndex], initialPositions[currentWeaponIndex]);
        }

        isSwitching = false;
    }

    private IEnumerator MoveWeapon(GameObject weapon, Vector3 targetPosition)
    {
        while (Vector3.Distance(weapon.transform.localPosition, targetPosition) > 0.01f)
        {
            weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, targetPosition, switchSpeed * Time.deltaTime);
            yield return null;
        }

        weapon.transform.localPosition = targetPosition;
    }
}
