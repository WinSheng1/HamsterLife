using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    public GameObject loadingUI;
    private LoadingScreenController loadingScreenController;

    private void Start()
    {
        loadingUI.SetActive(false);
    }

    public void OnStartButtonPressed()
    {
        loadingUI.SetActive(true);
        loadingScreenController = loadingUI.GetComponent<LoadingScreenController>();
        loadingScreenController.LoadScene("SampleScene");
    }
}
