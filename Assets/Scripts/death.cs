using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public GameObject deathParticles; // ������ ������ ������
    public Animator deathAnimator; // ��������� ��������� ��� �������� ������
    private Rigidbody rb; // ��������� Rigidbody ��� ��������� ��������
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDead)
        {
            // ��������� ����� isDead � true, ����� �������� ���������� ������
            isDead = true;

            // ��������� �������� �������
            rb.isKinematic = true;

            // ��������� ������� � ��������� ������
            deathParticles.SetActive(true);

            // ��������� �������� ������ � ���������� �������� idle
            deathAnimator.SetTrigger("Death");
            deathAnimator.SetBool("IsDead", true);

            // �������� ���������� �������� � ������ ������
            float deathAnimationDuration = deathAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            float particleDuration = deathParticles.GetComponent<ParticleSystem>().main.duration;
            float maxDuration = Mathf.Max(deathAnimationDuration, particleDuration);
            Invoke("RestartScene", maxDuration);
        }
    }

    private void RestartScene()
    {
        // ���������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}