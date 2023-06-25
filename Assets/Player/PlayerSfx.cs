using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerSfx : MonoBehaviour
{

    StarterAssetsInputs inputs;
    [SerializeField] AudioSource walkSfx;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        Debug.Log($"{inputs != null} {walkSfx != null}");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.move.x != 0 || inputs.move.y != 0) {
            if (walkSfx && !walkSfx.isPlaying){
                walkSfx.Play();
            }
        } else {
            if (walkSfx && walkSfx.isPlaying) {
                walkSfx.Stop();
            }
        }
    }
}
