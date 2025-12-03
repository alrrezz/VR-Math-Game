using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{

    //configuration parameters------------------------------------------------------------------------

    [SerializeField] AudioClip correctAnswerSound;
    [Range(0, 1)][SerializeField] float correctAnswerSoundVolume = 0.5f;
    [SerializeField] AudioClip wrongAnswerSound;
    [Range(0, 1)][SerializeField] float wrongAnswerSoundVolume = 0.5f;
    [SerializeField] AudioClip targetNumberSound;
    [Range(0, 1)][SerializeField] float targetNumberSoundVolume = 0.5f;

    //------------------------------------------------------------------------------------------------


    public void PlayCorrectSFX()
    {
        AudioSource.PlayClipAtPoint(correctAnswerSound, Camera.main.transform.position, correctAnswerSoundVolume);
    }
    public void PlayWrongSFX()
    {
        AudioSource.PlayClipAtPoint(wrongAnswerSound, Camera.main.transform.position, wrongAnswerSoundVolume);
    }
    public void PlayTargetNumberChangedSFX()
    {
        AudioSource.PlayClipAtPoint(targetNumberSound, Camera.main.transform.position, targetNumberSoundVolume);
    }
}
