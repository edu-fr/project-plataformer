using UnityEngine;
using UnityEngine.SceneManagement;
namespace ProjectPlataformer
{
    public class Menu_Play_Button : MonoBehaviour
    {
        public void Was_clicked()
        {
            SceneManager.LoadScene((int)Game_Scenes.Game_Scene, LoadSceneMode.Single);
        }
    }
}