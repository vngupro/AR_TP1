using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartCarGame()
    {
        SceneManager.LoadScene("CarGame");
    }
    
    public void StartFaceDetection()
    {
        SceneManager.LoadScene("FaceDetection");
    }

    public void StartPlaneDetection()
    {
        SceneManager.LoadScene("PlaneDetection");
    }
    
    public void StartImageTracking()
    {
        SceneManager.LoadScene("ImageTracking");
    }

    public void StartSnapFilter()
    {
        SceneManager.LoadScene("SnapFilter");
    }

    public void StartSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
