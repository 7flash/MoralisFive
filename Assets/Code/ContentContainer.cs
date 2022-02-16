using TMPro;
using UnityEngine;

public class ContentContainer {
    GameObject contentObj;

    public ContentContainer() {
        contentObj = GameObject.Find(Objects.content);

        contentObj.GetComponent<TextMeshProUGUI>().text = "Initialize...";
    }

    public void SetText(string text) {
        contentObj.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void DefaultContent() {
        string text = "";
        text += "Your address: 0x123\n";
        text += "Partner address: 0x456\n";
        text += "Your balance: 5\n";
        text += "Partner balance: 10\n";

        SetText(text);
    }
}
