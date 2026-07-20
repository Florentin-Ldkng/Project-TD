using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI MapText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMapState(int Wave, int Map)
    {
        WaveText.text = $"Wave: {Wave}";
        MapText.text = $"Map: {Map}";
    }
}
