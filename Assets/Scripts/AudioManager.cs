using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPlataformer
{
    public class TempClip : MonoBehaviour
    {
        public AudioSource Source;

        public void PlayAuido(AudioSource source)
        {
            Source = source;
            StartCoroutine(Destroywhendone());
        }
        IEnumerator Destroywhendone()
        {

            Source.Play();
            do yield return null; while (Source.isPlaying);
            Destroy(gameObject);
        }
    }
    public class AudioManager
    {
        public static void PlaySound(AudioClip audio, Vector2 where, int CilpVolume = 100)
        {
            if (audio == null) { return; }



            TempClip tepCilp = new GameObject(audio.name).AddComponent<TempClip>();
            Object.DontDestroyOnLoad(tepCilp);//when changing scenes it doesn't stop the audio.
            tepCilp.gameObject.transform.position = where;

            AudioSource AudioSource = tepCilp.gameObject.AddComponent<AudioSource>();
            AudioSource.clip = audio;

            FixAudio(AudioSource, CilpVolume);


            tepCilp.PlayAuido(AudioSource);




            static void FixAudio(AudioSource fix, int Volume)
            {
                fix.volume = (Volume / 100f) * (Settings.CurrentSettings.SXF_Volume / 100f);//Set Volume
                fix.pitch = Random.Range(0.8f, 1.2f);//change the sound a bit.
            }
        }
    }
}