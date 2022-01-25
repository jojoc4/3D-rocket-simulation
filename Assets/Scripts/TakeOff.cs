using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeOff : MonoBehaviour
{
    private float speed = 0f;
    private float altitude = 0f;
    private States state = States.IDLE;
    private DateTime startTime;
    private CameraSwitcher hatemSwitcher;
    private Boolean countdownLaunched=false;

    public float thrust = 1f;

    public Rigidbody fairing1;
    public Rigidbody fairing2;
    public Rigidbody second_stage;
    public Rigidbody first_stage;

    public ParticleSystem flamesSecondStage;
    public ParticleSystem flamesFirstStage;

    public AudioSource firstStageSound;
    public AudioSource countdownAudioSource;

    public UnityEngine.UI.Text speedText;
    public UnityEngine.UI.Text altitudeText;
    public UnityEngine.UI.Text stateText;
    public UnityEngine.UI.Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        hatemSwitcher = this.GetComponent<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        flightStateManager();

        if (state == States.LIFTOFF)
        {
            fairing1.AddForce(0, thrust, 0, ForceMode.Acceleration);
            fairing2.AddForce(0, thrust, 0, ForceMode.Acceleration);
            second_stage.AddForce(0, thrust, 0, ForceMode.Acceleration);
            first_stage.AddForce(0, thrust, 0, ForceMode.Acceleration);

            this.altitude = second_stage.transform.position.y;
            this.speed = thrust * (float)(DateTime.Now - startTime).TotalSeconds;
        }
        else if (state == States.SECOND_STAGE_START)
        {
            second_stage.AddForce(0, thrust, 0, ForceMode.Acceleration);

            this.altitude = second_stage.transform.position.y;
            this.speed = thrust * (float)(DateTime.Now - startTime).TotalSeconds;
        }

        speedText.text = "Vitesse : " + (int)(speed * 3.6) + " km/h";
        altitudeText.text = "Altitude : " + (altitude / 500).ToString("0.00") + " km";
        stateText.text = "Etat : " + state;
    }

    void flightStateManager()
    {
        switch(state)
        {
            case States.COUNTDOWN:
                if (!countdownLaunched)
                {
                    countdownLaunched = true;
                    countdownAudioSource.Play();
                }
                countdownText.enabled = true;
                countdownText.text = (10 - (int)(DateTime.Now - startTime).TotalSeconds).ToString();
                if ((DateTime.Now - startTime).TotalSeconds >= 10)
                {
                    state = States.LIFTOFF;
                    startTime = DateTime.Now;
                    countdownText.enabled = false;
                    flamesFirstStage.Play();
                    firstStageSound.Play();
                }
                break;
            case States.LIFTOFF:
                fairing1.useGravity = true;
                fairing2.useGravity = true;
                second_stage.useGravity = true;
                first_stage.useGravity = true;

                if (altitude >= 90 && altitude < 900)
                    hatemSwitcher.switchCam(CameraSwitcher.Cam.LAUNCHPAD_DISTANT);
                if (altitude >= 900)
                    hatemSwitcher.switchCam(CameraSwitcher.Cam.FS_TOP);
                if (altitude >= 1000)
                    state = States.MAIN_CUTOFF;
                break;

            case States.MAIN_CUTOFF:
                hatemSwitcher.switchCam(CameraSwitcher.Cam.FS_TOP);
                flamesFirstStage.Stop();
                firstStageSound.Stop();
                state = States.SEPARATION;
                break;
            case States.SEPARATION:
                fairing1.AddForce(-3, 1, 0, ForceMode.Impulse);
                fairing2.AddForce(3, 1, 0, ForceMode.Impulse);
                state = States.SECOND_STAGE_START;
                break;
            case States.SECOND_STAGE_START:
                hatemSwitcher.switchCam(CameraSwitcher.Cam.SS_BOTTOM);
                flamesSecondStage.Play();
                if (!firstStageSound.isPlaying)
                    firstStageSound.Play();
                break;
        }
    }

    public void startFlight()
    {
        startTime = DateTime.Now;
        state = States.COUNTDOWN;
    }
}

public enum States
{
    IDLE,
    COUNTDOWN,
    LIFTOFF,
    MAIN_CUTOFF,
    SEPARATION,
    SECOND_STAGE_START
}