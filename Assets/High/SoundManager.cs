using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource fireSFX;

    public AudioSource roseSFX;

    public AudioSource shockSFX;

    static SoundManager instance;


    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        Init();
    }

    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
          //  Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
               GameObject go =  new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float vol = 1.0f)
    {

        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Resources.Load<AudioClip>(path);
            if(audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            audioSource.volume = vol;

            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = audioClip;
            audioSource.Play();

        }
        else
        {

            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.volume = vol;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Resources.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }
        return audioClip;

    }

    //사용예
    
}
