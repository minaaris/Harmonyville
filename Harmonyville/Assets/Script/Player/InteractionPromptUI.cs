using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera MainCam;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TextMeshProUGUI PromptText;

    private void Start()
    {
        MainCam = Camera.main;
        UIPanel.SetActive(false);
    }
    private void LateUpdate()
    {
        var rotation = MainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    public bool IsDisplayed = false;
   
    public void SetUPrompt(string promptText)
    {
        PromptText.text = promptText;
        UIPanel.SetActive(true);
        IsDisplayed = true;
    }
    public void Close()
    {
        UIPanel.SetActive(false);
        IsDisplayed = false;
    }
}
