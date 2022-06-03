using UnityEngine;
using System.IO;
namespace ProjectPlataformer
{
    [System.Serializable]
    public class Settings
    {
        public float SXF_Volume = 80f;
        public float Music_Volume = 80f;

        private static Settings _settings;

        public static readonly string SettingsPath = Application.persistentDataPath + "/Settings.json";
        public static Settings CurrentSettings
        {
            get
            {
                if (_settings == null)
                {

                    if (File.Exists(SettingsPath))
                    {
                        try
                        {
                            string json = File.ReadAllText(SettingsPath);
                            _settings = JsonUtility.FromJson<Settings>(json);
                        }
                        catch (System.Exception ex)
                        {

                            Debug.LogWarning("Loading Settings some how failed:" + ex.ToString());
                            return new Settings();
                        }
                    }
                    else
                    {
                        _settings = new Settings();
                        SaveCurrentSettings();
                    };


                }
                return _settings;
            }
        }
        public static void SaveCurrentSettings()
        {
            var settings = CurrentSettings;
            try
            {
                string json = JsonUtility.ToJson(settings);
                File.WriteAllText(SettingsPath, json);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Saveing Settings some how failed:" + ex.ToString());

            }

        }
    }
    public enum Game_Scenes
    {
        Menu_Scene,
        Game_Scene,
    }
}