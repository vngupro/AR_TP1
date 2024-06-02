using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;

public class ARFaceSwitch : MonoBehaviour
{
    public ARFaceManager aRFaceManager;
    public List<GameObject> facePrefabs;
    public GameObject buttonPrefab;
    public GameObject uiFaces;
    
    private void Start()
    {
        int index = 1;
        foreach(var facePrefab in facePrefabs)
        {
            // Create a button in ui
            GameObject button = Instantiate(buttonPrefab, uiFaces.transform);
            button.GetComponent<Button>().onClick.AddListener(() => SwitchFace(facePrefabs.IndexOf(facePrefab)));
            button.GetComponentInChildren<TMP_Text>().text = index.ToString();
            index++;
        }
    }

    public void SwitchFace(int index)
    {
        aRFaceManager.facePrefab = facePrefabs[index];
        foreach (ARFace aRFace in aRFaceManager.trackables)
        {
            Destroy(aRFace.gameObject);

        }
    }
}
