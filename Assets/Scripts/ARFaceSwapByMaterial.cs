using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ARFaceManager))]
public class ARFaceSwapByMaterial : MonoBehaviour
{
    [Header("Face Materials")]
    public List<Material> faceMaterials;

    [Header("UI")]
    public GameObject uiFaces;
    public GameObject uiButtonPrefab;

    [Header("Debug")]
    [SerializeField] private ARFaceManager faceManager;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    private void Start()
    {
        int index = 0;
        foreach (var faceMaterial in faceMaterials)
        {
            // Create a button in ui
            GameObject button = Instantiate(uiButtonPrefab, uiFaces.transform);
            button.GetComponent<Button>().onClick.AddListener(() => SwapFace(faceMaterials.IndexOf(faceMaterial)));
            button.GetComponentInChildren<TMP_Text>().text = index.ToString();
            index++;
        }
    }

    public void SwapFace(int index)
    {
        foreach(ARFace aRFace in faceManager.trackables)
        {
            aRFace.GetComponent<Renderer>().material = faceMaterials[index];
        }
    }
}
