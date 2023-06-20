using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerDeathHandler : MonoBehaviour
{

    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] FirstPersonController playerInputController;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    public void HandleDeath() {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        playerInputController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
