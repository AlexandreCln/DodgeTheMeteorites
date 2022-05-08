using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _mainTheme;
    [SerializeField] private float _mainThemeMaxDifficultyPitch = 1.3f;
    private AudioMixerGroup _pitchBendGroup;

    void Start()
    {
        if (true)
        {
            _pitchBendGroup = Resources.Load<AudioMixerGroup>("Mixers/Pitch Bend Mixer");
            _mainTheme.outputAudioMixerGroup = _pitchBendGroup;
            _mainTheme.Play();
            StartCoroutine(IncreasePitchWithDifficulty());
        }
    }

    void Update()
    {
        if (Globals.instance.PlayerIsDead)
        {
            _mainTheme.Stop();
        }
    }

    IEnumerator IncreasePitchWithDifficulty()
    {
        while (Difficulty.GetDifficultyPercent() < 1)
        {
            if (Globals.instance.PlayerIsDead)
                yield return null;

            float pitch = Mathf.Lerp(1f, _mainThemeMaxDifficultyPitch, Difficulty.GetDifficultyPercent());
            _mainTheme.pitch = pitch; 
            _pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / pitch);
            yield return new WaitForSeconds(15);
        }
    }
}
