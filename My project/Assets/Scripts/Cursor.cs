using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] CanvasScaler canvasScaler;
    [SerializeField] RectTransform cursor;
    [SerializeField] Sprite normalHand;
    [SerializeField] Sprite grabbingHand;
    [SerializeField] Image openHand;

    Vector3 lastMousePosition;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        UnityEngine.Cursor.visible = false;
        cursor.anchoredPosition = UnscalePosition(Input.mousePosition);
        

        transform.localScale = Vector3.one * (1 + (Input.mousePosition - lastMousePosition).sqrMagnitude*0.0002f);
        transform.rotation = Quaternion.Euler(0, 0, (Input.mousePosition - lastMousePosition).x * -0.4f);
        lastMousePosition = Input.mousePosition;
    }



    public void CloseHand()
    {
        openHand.enabled = false;
    }
    

    public Vector2 UnscalePosition(Vector2 vec)
    {
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;

        float ratio = Mathf.Lerp(heightRatio, widthRatio, canvasScaler.matchWidthOrHeight);

        return vec / ratio;
    }
}