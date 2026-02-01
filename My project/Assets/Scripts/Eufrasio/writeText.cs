using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class writeText : MonoBehaviour
{
    public float delay = 0.1f;
    TextMeshProUGUI textBox;
    public bool terminado;

    bool pulsado;

  
    IEnumerator ShowText()
    {
        while (textBox.maxVisibleCharacters < textBox.text.Length)
        {
            textBox.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);

        }
    }
    private void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = "";
        textBox.maxVisibleCharacters = 0;
        textBox.text = textBox.text.Replace("\\n", "\n");
        
        StartCoroutine(ShowText());


    }

    public void changeText(string newText)
    {
        textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = newText;
        textBox.maxVisibleCharacters = 0;
        textBox.text = textBox.text.Replace("\\n", "\n");

        StartCoroutine(ShowText());
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            changeText("hola hola caracola");

        }
        if (Input.GetKeyDown(KeyCode.K))
        {

            changeText("adios adios yoguron");

        }

    }
}





