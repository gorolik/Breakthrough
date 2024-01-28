using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }
}
