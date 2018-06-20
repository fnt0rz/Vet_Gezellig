using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointDisplay : MonoBehaviour {

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    public void DisplayCheckPointText()
    {
        StartCoroutine(StartDisplay());
    }

    private IEnumerator StartDisplay()
    {
        text.enabled = true;
        yield return new WaitForSeconds(1.5f);
        text.enabled = false;
    }

}
