using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public GameObject deathParticles; // Префаб частиц смерти
    public Animator deathAnimator; // Компонент аниматора для анимации смерти
    private Rigidbody rb; // Компонент Rigidbody для остановки движения
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDead)
        {
            // Установка флага isDead в true, чтобы избежать повторного вызова
            isDead = true;

            // Остановка движения объекта
            rb.isKinematic = true;

            // Включение объекта с частицами смерти
            deathParticles.SetActive(true);

            // Включение анимации смерти и отключение анимации idle
            deathAnimator.SetTrigger("Death");
            deathAnimator.SetBool("IsDead", true);

            // Ожидание завершения анимации и частиц смерти
            float deathAnimationDuration = deathAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            float particleDuration = deathParticles.GetComponent<ParticleSystem>().main.duration;
            float maxDuration = Mathf.Max(deathAnimationDuration, particleDuration);
            Invoke("RestartScene", maxDuration);
        }
    }

    private void RestartScene()
    {
        // Перезапуск сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}