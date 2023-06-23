using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mediumAmmoDisplay;
    [SerializeField] TextMeshProUGUI largeAmmoDisplay;
    [SerializeField] TextMeshProUGUI lightChargeDisplay;
    [SerializeField] TextMeshProUGUI specialItemDisplay;
    [SerializeField] float displayDuration = 5f;
    [SerializeField] float displayFadeDuration = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        mediumAmmoDisplay.enabled = false;
        largeAmmoDisplay.enabled = false;
        specialItemDisplay.enabled = false;
        lightChargeDisplay.enabled = false;
    }

    // Update is called once per frame
    public void DisplayLoot(int medAmmo, int largeAmmo, float rechargePct, bool isKey) {
        SetupMediumDisplay(medAmmo);
        SetupLargeDisplay(largeAmmo);
        SetupLightDisplay(rechargePct);
        SetupSpecialDisplay(isKey);
        StartCoroutine(ManageDisplay(mediumAmmoDisplay));
        StartCoroutine(ManageDisplay(largeAmmoDisplay));
        StartCoroutine(ManageDisplay(lightChargeDisplay));
        if (isKey) {
            StartCoroutine(ManageDisplay(specialItemDisplay));
        }
        
    }

    private void SetupMediumDisplay(int medAmmo) {
        string medDisplay = $"Looted {medAmmo} Medium ammo.";
        mediumAmmoDisplay.text = medDisplay;
        StartCoroutine(ManageDisplay(mediumAmmoDisplay));
    }

    private void SetupLargeDisplay(int largeAmmo) {
        string largeDisplay = $"Looted {largeAmmo} Large ammo.";
        largeAmmoDisplay.text = largeDisplay;
        StartCoroutine(ManageDisplay(largeAmmoDisplay));
    }

    private void SetupLightDisplay(float rechargePct) {
        string lightDisplay = $"Recovered {Mathf.RoundToInt(rechargePct * 100)}% light battery.";
        lightChargeDisplay.text = lightDisplay;
        StartCoroutine(ManageDisplay(lightChargeDisplay));
    }

    private void SetupSpecialDisplay(bool isKey) {
        StartCoroutine(ManageDisplay(mediumAmmoDisplay));
    }

    private IEnumerator ManageDisplay(TextMeshProUGUI text) {
        text.enabled = true;
        yield return new WaitForSeconds(displayDuration);
        Color c = text.color;
        float elapsedTime = 0.0f;
        while (elapsedTime < displayFadeDuration) {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / displayFadeDuration);
            text.color = c;
        }
        text.enabled = false;
        c.a = 1.0f;
        text.color = c;
    }
}
