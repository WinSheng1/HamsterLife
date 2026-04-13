using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimerEnd : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private string mainScene;
    private bool hasLoadedScene = false;

    private void Start()
    {
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }
    }

    private void Update()
    {
        if (director != null && director.state == PlayState.Playing && !hasLoadedScene)
        {
            // Check if timeline has finished playing
            if (director.time >= director.duration)
            {
                hasLoadedScene = true;
                SceneManager.LoadScene(mainScene);
            }
        }
    }
}
