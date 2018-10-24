using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddText : MonoBehaviour
{

    public static GameObject AddSide(int size, string text)
    {
        GameObject mainObj = new GameObject();
        Canvas canvasObj = mainObj.AddComponent<Canvas>();
        canvasObj.renderMode = RenderMode.WorldSpace;

        GameObject childObj2 = new GameObject();
        RawImage rawimageObj = childObj2.AddComponent<RawImage>();
        rawimageObj.rectTransform.SetSizeWithCurrentAnchors
             (RectTransform.Axis.Horizontal, size);
        rawimageObj.rectTransform.SetSizeWithCurrentAnchors
             (RectTransform.Axis.Vertical, size);
        rawimageObj.color = Color.yellow;
        childObj2.transform.SetParent(mainObj.transform, false);
        GameObject childObj1 = new GameObject();
        Text textObj = childObj1.AddComponent<Text>();
        textObj.font = (Font)Resources.GetBuiltinResource
            (typeof(Font), "Arial.ttf"); ;
        textObj.text = text;
        textObj.alignment = TextAnchor.MiddleCenter;
        textObj.enabled = true;
        textObj.fontSize = (int)(size * 0.8);
        textObj.color = Color.black;
        textObj.rectTransform.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Horizontal, size);
        textObj.rectTransform.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Vertical, size);
        childObj1.transform.SetParent(mainObj.transform, false);

        return mainObj;
    }
}
