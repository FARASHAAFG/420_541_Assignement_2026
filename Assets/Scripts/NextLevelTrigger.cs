using UnityEngine;
using UnityEngine.SceneManagement;
 
public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
 
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
 
        ScoreManager.Instance.SaveFinalScore();
 
        SceneManager.LoadScene(nextSceneName);
    }
}
