using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace ProjectPlataformer
{
    public class Settings_Slider : MonoBehaviour
    {
        private enum Type
        {
            SXF,
            Music,
        }
        [SerializeField] private Slider slider;

        [SerializeField] private Type SliderType;
        private Slider.SliderEvent m_MyEvent;
        private void Start()
        {

            if (SliderType == Type.SXF)
            {
                slider.value = Settings.CurrentSettings.SXF_Volume;
            }
            else
            {
                slider.value = Settings.CurrentSettings.Music_Volume;
            }


            m_MyEvent = new Slider.SliderEvent();
            m_MyEvent.AddListener(OnValueUpdate);
            slider.onValueChanged = m_MyEvent;
        }
        private void OnValueUpdate(float value)
        {
            if (SliderType == Type.SXF)
            {
                Settings.CurrentSettings.SXF_Volume = value;
            }
            else
            {
                Settings.CurrentSettings.Music_Volume = value;
            }


            Settings.SaveCurrentSettings();
        }
    }
}