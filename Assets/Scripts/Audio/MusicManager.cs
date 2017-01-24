using UnityEngine;
using System.Collections.Generic;

public enum TypeOfMusic
{
    //Insert music name exemple: IntroGame,
}

public class MusicManager : MonoBehaviour
{

    static MusicManager m_instance;
    static AudioSource myAudioSource;

    private static Dictionary<TypeOfMusic, string> PlayListDic = new Dictionary<TypeOfMusic, string>() {
        //Insert music pair name + path exemple {TypeOfMusic.IntroGame,"Music/Blaze Intro Game" },
    };

    private static List<AudioClip> PlayList = new List<AudioClip>();

    public static MusicManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType(typeof(MusicManager)) as MusicManager;
            }
            return m_instance;
        }
    }


    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        for (int i = 0; i < PlayListDic.Count; i++)
        {
            PlayList.Add(Resources.Load<AudioClip>(PlayListDic[(TypeOfMusic)i]));
        }
    }

    public void Play(TypeOfMusic index, bool isLooping = false, float balance = 1f)
    {
        if (PlayList.Count > 0)
        {
            myAudioSource.clip = PlayList[(int)index];
            myAudioSource.Play();
            myAudioSource.volume = balance;
            myAudioSource.loop = isLooping;
        }
    }

    public static void Stop()
    {
        if (myAudioSource)
            myAudioSource.Stop();
    }

}
