using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    public Transform target; // Ссылка на объект, за которым следим (персонаж)
    public ParticleSystem particleSystem; // Ссылка на систему частиц

    private Rigidbody targetRigidbody;
    private bool isParticleActive = false;

    private void Start()
    {
        // Выключаем систему частиц при старте
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // Получаем ссылку на компонент Rigidbody персонажа
        if (target != null)
        {
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // Проверяем, двигается ли персонаж и находится ли он на земле
        if (target != null && targetRigidbody != null)
        {
            bool isGrounded = Physics.Raycast(target.position, Vector3.down, 0.1f); // Проверяем, находится ли персонаж на земле

            if (targetRigidbody.velocity.magnitude > 0.1f)
            {
                // Если персонаж движется и на земле, включаем систему частиц и позиционируем её за персонажем
                if (!isParticleActive)
                {
                    particleSystem.Play();
                    isParticleActive = true;
                }

                // Позиционируем систему частиц за персонажем
                Vector3 targetPosition = target.position - target.forward * 1.0f; // Располагаем систему частиц немного позади персонажа
                particleSystem.transform.position = targetPosition;
            }
            else
            {
                // Если персонаж стоит на месте или находится в воздухе, выключаем систему частиц
                if (isParticleActive)
                {
                    particleSystem.Stop();
                    isParticleActive = false;
                }
            }
        }
    }
}
