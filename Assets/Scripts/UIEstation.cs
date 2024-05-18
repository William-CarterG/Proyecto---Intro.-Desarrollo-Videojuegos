using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEstation : MonoBehaviour
{
    // Start is called before the first frame update
    public string LevelStation;
    private TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = LevelStation;
    }
}
