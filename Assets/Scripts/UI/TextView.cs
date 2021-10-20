using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextView : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void SetText(int text)
    {
        _text.text = text.ToString();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
