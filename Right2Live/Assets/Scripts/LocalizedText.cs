using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    string _textKey;
    TextMeshProUGUI _UIText;
    // Start is called before the first frame update
    void Start()
    {
        _UIText = GetComponent<TextMeshProUGUI>();
        _UIText.text = LocalizationManager.instance.getEntry(_textKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
