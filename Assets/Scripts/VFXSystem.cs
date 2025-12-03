using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSystem : MonoBehaviour
{
    //config parameters
    [SerializeField] GameObject correctExplosionParticelPrefab;
    [SerializeField] GameObject wrongExplosionParticelPrefab;
    [SerializeField] float durationOfExplosion = 1f;


    public void CorrectExplosionVFX(Vector3 position)
    {
        var explosionVFX = Instantiate
            (correctExplosionParticelPrefab,
            position,
            Quaternion.identity);

        Destroy
        (explosionVFX,
        durationOfExplosion);
    }
    public void WrongExplosionVFX(Vector3 position)
    {
        var explosionVFX = Instantiate
            (wrongExplosionParticelPrefab,
            position,
            Quaternion.identity);

        Destroy
        (explosionVFX,
        durationOfExplosion);
    }
}
