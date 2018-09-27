using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class Sound_Manager
    {
    private static Sound_Manager m_instance;
    private bool m_bInitializeOnce;

    AudioSource  m_myAudio;

    Dictionary<string, AudioClip> m_dicSound;

    // Getter, Setter
    public static Sound_Manager GetInstance()
    {
        if (null == m_instance)
            m_instance = new Sound_Manager();

        return m_instance;
    }
     
    public void Initialize()
    {
        m_myAudio = GameObject.Find("Audio").GetComponent<AudioSource>();

        if (m_bInitializeOnce)
            return;
        m_bInitializeOnce = true;

        m_dicSound = new Dictionary<string, AudioClip>();

        AudioClip[] audios = Resources.LoadAll<AudioClip>("Sound");

        for (int i = 0; i < audios.Length; ++i)
            m_dicSound.Add(audios[i].name, audios[i]);
    }

    public void PlaySound (string name)
    {
        if (!m_dicSound.ContainsKey(name))
            return;

        if(null == m_myAudio)
            m_myAudio = GameObject.Find("Audio").GetComponent<AudioSource>();

        m_myAudio.PlayOneShot (m_dicSound[name]);
    }

    public void Stop_Sound()
    {
        m_myAudio.Stop();
    }
}


