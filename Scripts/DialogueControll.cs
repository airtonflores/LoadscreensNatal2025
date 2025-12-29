using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/*Esse é o coração da cutscene
*/

public class DialogueControll : MonoBehaviour
{
    public TimerController scriptDoTimer;

    public CinemachineCamera camAlta;
    public CinemachineCamera camBaixa;
    public CinemachineCamera camTV;

    public GameObject Dialoguebox;
    public GameObject TextoBackup;
    public GameObject TextoNatal;

    public GameObject CameraKakau;
    public GameObject CameraAlta;

    public GameObject globalVoluveCameraAlta;

    public Volume globalVolume;
    private Vignette vignette;

    public TimerController meuTimer;

    void Start()
    {
        if (globalVolume != null)
        {
            globalVolume.profile.TryGet(out vignette);
        }
        else
        {
            Debug.LogError("Esqueceu de arrastar o Global Volume no Inspector!");
        }

        if (vignette != null)
        {
            StartCoroutine(DialogueBlok());
        }
    }

    public IEnumerator DialogueBlok()
    {
        if (vignette == null) yield break;


        vignette.smoothness.value = 0.137f;
        meuTimer.timerIsRunning = true;

        Dialoguebox.SetActive(true);
        yield return new WaitForSeconds(1);
        TextoBackup.SetActive(true);

        GameObject npcObjectF0 = GameObject.Find("GamblerCat");
        Animator npcAnimatorF0 = npcObjectF0.GetComponentInChildren<Animator>();
        npcAnimatorF0.Play("IdleAction");

        yield return new WaitForSeconds(6);

        npcObjectF0 = GameObject.Find("GamblerCat");
        npcAnimatorF0 = npcObjectF0.GetComponentInChildren<Animator>();
        npcAnimatorF0.Play("IdleAction2");

        camAlta.Priority = 20;
        camBaixa.Priority = 10;

        yield return new WaitForSeconds(1);
        vignette.smoothness.value = 0.137f;

        globalVoluveCameraAlta.SetActive(true);

        yield return new WaitForSeconds(1);
        TextoBackup.SetActive(false);

        camAlta.Priority = 10;
        camBaixa.Priority = 20;
        globalVoluveCameraAlta.SetActive(false);

        yield return new WaitForSeconds(1);

        StartCoroutine(DesligDia());
    }

    private IEnumerator DesligDia()
    {
        TextoBackup.SetActive(false);
        TextoNatal.SetActive(true);

        GameObject npcObjectF0 = GameObject.Find("GamblerCat");
        Animator npcAnimatorF0 = npcObjectF0.GetComponentInChildren<Animator>();
        npcAnimatorF0.Play("Hit3");

        yield return new WaitForSeconds(5);

        npcObjectF0 = GameObject.Find("GamblerCat");
        npcAnimatorF0 = npcObjectF0.GetComponentInChildren<Animator>();
        npcAnimatorF0.Play("Spell");

        TextoNatal.SetActive(false);

        yield return new WaitForSeconds(0.7f);
        Dialoguebox.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        camBaixa.Priority = 10;
        camTV.Priority = 20;
        vignette.smoothness.value = 0.634f;



        yield return new WaitForSeconds(9.3f);

        camBaixa.Priority = 20;
        camTV.Priority = 10;

        yield return new WaitForSeconds(0.7f);

        scriptDoTimer.ResetTimer();
        StartCoroutine(DialogueBlok());
        yield return null;
    }
}