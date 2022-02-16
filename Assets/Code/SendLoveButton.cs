using UnityEngine;
using UnityEngine.UI;

public class SendLoveButton {
    public GameObject sceneObject;
    public Button buttonComponent;
    public SendLoveButton() {
        sceneObject = GameObject.Find(Objects.sendLove);

        buttonComponent = sceneObject.GetComponent<Button>();
    }
}