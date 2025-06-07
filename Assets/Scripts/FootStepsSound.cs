using System.Collections;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    public AudioSource grassFoot1, grassFoot2;
    public AudioSource concreteFoot1, concreteFoot2;

    public float stepRate = 0.5f; // tiempo entre pasos
    private bool stepToggle = false;

    private string currentSurface = "Grass"; // por defecto
    private Coroutine footstepCoroutine;

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving && footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootsteps());
        }
        else if (!isMoving && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }

        DetectSurface();
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (currentSurface == "Grass")
            {
                if (stepToggle)
                    grassFoot1.Play();
                else
                    grassFoot2.Play();
            }
            else if (currentSurface == "Concrete")
            {
                if (stepToggle)
                    concreteFoot1.Play();
                else
                    concreteFoot2.Play();
            }

            stepToggle = !stepToggle;
            yield return new WaitForSeconds(stepRate);
        }
    }

    void DetectSurface()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                currentSurface = "Grass";
            }
            else if (hit.collider.CompareTag("Concrete"))
            {
                currentSurface = "Concrete";
            }
        }
    }
}
