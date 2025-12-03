using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private float cubeDestroydelay = 0.3f;

    private HandleScore handleScore;
    private SFXPlayer soundPlayer;
    private VFXSystem vfxSystem;
    private GameSession gameSession;

    private void Start()
    {
        handleScore = GetComponent<HandleScore>();
        soundPlayer = GetComponent<SFXPlayer>();
        vfxSystem = GetComponent<VFXSystem>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MathCube")) return;

        MathCube mathCube = other.GetComponent<MathCube>();
        if (mathCube == null || mathCube.isHit) return;

        mathCube.isHit = true;
        if (mathCube.pairedCube != null)
        {
            mathCube.pairedCube.isHit = true;
        }

        HandleCubeAnswer(mathCube);
    }

    private void HandleCubeAnswer(MathCube mathCube)
    {
        bool isCorrect = mathCube.mathTask.IsCorrect();

        if (isCorrect)
        {
            soundPlayer.PlayCorrectSFX();
            handleScore.AddScoreForCorrectAnswer(mathCube);
            vfxSystem.CorrectExplosionVFX(mathCube.transform.position);
            vfxSystem.CorrectExplosionVFX(mathCube.pairedCube.transform.position);
        }
        else
        {
            soundPlayer.PlayWrongSFX();
            vfxSystem.WrongExplosionVFX(mathCube.transform.position);
            vfxSystem.WrongExplosionVFX(mathCube.pairedCube.transform.position);
            gameSession.LoseLife();
        }

        Destroy(mathCube.gameObject, cubeDestroydelay);
        Destroy(mathCube.pairedCube.gameObject, cubeDestroydelay);
    }
}
