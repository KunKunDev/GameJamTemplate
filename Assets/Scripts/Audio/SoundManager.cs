using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TypeOfSound
{
    //Insert sound name here exempple : Explosion,
}

/// <summary>
/// Keep track of every type of sound and their coroutine
/// The trick here is to associate each type of sound with their coroutine so we can stop a specific type of sound
/// </summary>
public struct SoundsTracker
{
    public IEnumerator m_SoundCoroutine;
    public TypeOfSound m_TypeOfSound;

    public SoundsTracker(IEnumerator scl, TypeOfSound ts)
    {
        m_SoundCoroutine = scl;
        m_TypeOfSound = ts;
    }
}

public class SoundManager : MonoBehaviour
{
    static SoundManager m_instance;
    static float soundVolume = 1;

    private static Dictionary<TypeOfSound, string> SoundsListDic = new Dictionary<TypeOfSound, string>() {
        //Insert sounds here. exemple {TypeOfSound.Countdown,"SFX/Countdown" } 
    };

    private static List<AudioClip> SoundsList = new List<AudioClip>();
    private static List<SoundsTracker> m_SoundsCoroutine = new List<SoundsTracker>();

    void Awake()
    {
        for (int i = 0; i < SoundsListDic.Count; i++)
        {
            SoundsList.Add(Resources.Load<AudioClip>(SoundsListDic[(TypeOfSound)i]));
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            }
            return m_instance;
        }
    }

    public AudioSource PlaySound(TypeOfSound clipEnum, float pitch = 1, float balance = 1, bool loop = false)
    {
        if (Instance != null)
        {
            return Instance.StartSound(clipEnum, pitch, balance, loop);
        }
        else {
            return null;
        }
    }

    private AudioSource StartSound(TypeOfSound clipEnum, float pitch = 1, float balance = 1, bool loop = false)
    {
        AudioSource audioSource = null;
        if (Instance != null)
        {
            AudioClip clip = SoundsList[(int)clipEnum];
            if (clip == null) return null;
            audioSource = Instance.gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;

            m_SoundsCoroutine.Add(new SoundsTracker(Instance.PlaySoundCoroutine(audioSource, pitch, balance, loop), clipEnum));

            Instance.StartCoroutine(m_SoundsCoroutine[m_SoundsCoroutine.Count - 1].m_SoundCoroutine);
        }
        return audioSource;
    }

    IEnumerator PlaySoundCoroutine(AudioSource audioSource, float pitch = 1, float balance = 1, bool loop = false)
    {
        audioSource.volume = soundVolume;
        float endTime = 0;
        audioSource.loop = false;
        endTime = Time.time + audioSource.clip.length;

        audioSource.pitch = pitch;
        audioSource.volume = balance;
        audioSource.loop = loop;
        audioSource.Play();

        while (Time.time < endTime && audioSource.isPlaying)
        {
            yield return null;
        }
        if (!loop)
        {
            audioSource.Stop();
            Destroy(audioSource);
        }
    }

    public void SetSound(bool playSound)
    {
        if (playSound)
            soundVolume = 1.0f;
        else
            soundVolume = 0.0f;
    }

    public void StopAllSounds()
    {
        if (Instance != null)
        {
            StopAllCoroutines();
            for (int i = 0; i < SoundsListDic.Count; i++)
            {
                StopSounds((TypeOfSound)i);
            }
        }
    }

    public void StopSounds(TypeOfSound clipEnum)
    {
        if (Instance != null)
        {
            AudioSource[] m_AudioSource = GetComponents<AudioSource>();

            //End all coroutines
            for (int i = 0; i < m_SoundsCoroutine.Count; i++)
            {
                if (m_SoundsCoroutine[i].m_TypeOfSound == clipEnum)
                    StopCoroutine(m_SoundsCoroutine[i].m_SoundCoroutine);
            }

            //Destroy all components
            for (int i = 0; i < m_AudioSource.Length; i++)
            {
                if (m_AudioSource[i].clip == SoundsList[(int)clipEnum])
                    Destroy(m_AudioSource[i]);
            }
        }
    }
}
