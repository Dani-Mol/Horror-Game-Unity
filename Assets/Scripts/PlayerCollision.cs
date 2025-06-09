using UnityEngine;
using UnityEngine.UI; 
public class PlayerCollision : MonoBehaviour
{
    public GameObject loseScreen; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            loseScreen.SetActive(true);
            AudioListener.pause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

}
