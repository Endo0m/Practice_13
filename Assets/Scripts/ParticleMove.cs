using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    public Transform target; // ������ �� ������, �� ������� ������ (��������)
    public ParticleSystem particleSystem; // ������ �� ������� ������

    private Rigidbody targetRigidbody;
    private bool isParticleActive = false;

    private void Start()
    {
        // ��������� ������� ������ ��� ������
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // �������� ������ �� ��������� Rigidbody ���������
        if (target != null)
        {
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // ���������, ��������� �� �������� � ��������� �� �� �� �����
        if (target != null && targetRigidbody != null)
        {
            bool isGrounded = Physics.Raycast(target.position, Vector3.down, 0.1f); // ���������, ��������� �� �������� �� �����

            if (targetRigidbody.velocity.magnitude > 0.1f)
            {
                // ���� �������� �������� � �� �����, �������� ������� ������ � ������������� � �� ����������
                if (!isParticleActive)
                {
                    particleSystem.Play();
                    isParticleActive = true;
                }

                // ������������� ������� ������ �� ����������
                Vector3 targetPosition = target.position - target.forward * 1.0f; // ����������� ������� ������ ������� ������ ���������
                particleSystem.transform.position = targetPosition;
            }
            else
            {
                // ���� �������� ����� �� ����� ��� ��������� � �������, ��������� ������� ������
                if (isParticleActive)
                {
                    particleSystem.Stop();
                    isParticleActive = false;
                }
            }
        }
    }
}
