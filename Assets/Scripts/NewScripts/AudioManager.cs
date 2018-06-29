using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioSource Background;
    public AudioSource SFX_1;
    public AudioSource SFX_2;

    public AudioClip MenuMovement;
    public AudioClip MenuSelect;
    public AudioClip MenuStartGame;
    public AudioClip GameGridRotation;
    public AudioClip GameQuadCardMovement;
    public AudioClip GameCurveCardMovement;
    public AudioClip GameConfirm;
    public AudioClip GameSpawn;
    public AudioClip GameEnergy180;
    public AudioClip GameEnergyDoubleUpgrade;
    public AudioClip GameEnergyCollision;
    public AudioClip GameScoreQuad;
    public AudioClip GameScoreCurve;
    public AudioClip GameWin;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
