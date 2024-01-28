using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string _startLevel;


    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(_startLevel);
    }
}
