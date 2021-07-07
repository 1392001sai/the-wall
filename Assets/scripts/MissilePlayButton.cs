using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissilePlayButton : MonoBehaviour
{
    public GameObject fire;
    public Button missileButton;
    bool move = false;
    public float speed;

    public void OnClick()
    {
        fire.SetActive(true);
        missileButton.interactable = false;
        GameObject.FindWithTag("LevelPanel").GetComponent<Animator>().SetTrigger("ShiftRight");
        move = true;

    }

    private void Update()
    {
        if (move == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
