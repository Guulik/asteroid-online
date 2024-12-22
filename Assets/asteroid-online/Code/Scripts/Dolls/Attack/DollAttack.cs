using System.Collections;
using Dolls.Health;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dolls.Attack
{
    public class DollAttack : NetworkBehaviour
    {
        [SerializeField] private Camera characterCamera;

        [SerializeField] private int attackSpeed = 10; // Скорость атаки (выстрелов в минуту)
        [SerializeField] private float attackRange = 100f; // Дистанция стрельбы
        [SerializeField] private int damage = 20; // Урон от попадания

        private Coroutine _autoFireCoroutine;
        private PlayerInput _playerInput;

        private Player _playerOwner;
        private float _timeBetweenAttacks;

        private void Awake()
        {
            _timeBetweenAttacks = 60f / attackSpeed;
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Attack.started += OnShootButtonPressed;
            _playerInput.Player.Attack.canceled += OnShootButtonReleased;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.Attack.started -= OnShootButtonPressed;
            _playerInput.Player.Attack.canceled -= OnShootButtonReleased;
        }

        public void SetPlayerOwner(Player newOwner)
        {
            _playerOwner = newOwner;
        }

        private void OnShootButtonPressed(InputAction.CallbackContext context)
        {
            if (!IsOwner) return;
            _autoFireCoroutine ??= StartCoroutine(AutoFire());
        }

        private void OnShootButtonReleased(InputAction.CallbackContext context)
        {
            if (!IsOwner) return;
            if (_autoFireCoroutine != null)
            {
                StopCoroutine(_autoFireCoroutine);
                _autoFireCoroutine = null;
            }
        }

        /// <summary>
        ///     Корутин для автоматической стрельбы, пока кнопка нажата
        /// </summary>
        private IEnumerator AutoFire()
        {
            while (true)
            {
                Shoot(); // Вызываем стрельбу напрямую
                yield return new WaitForSeconds(_timeBetweenAttacks);
            }
        }

        /// <summary>
        ///     Метод для стрельбы Raycast'ом из центра экрана
        /// </summary>
        [ServerRpc(RequireOwnership = false)] // Выполняется на сервере
        private void Shoot()
        {
            Ray ray = characterCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Луч из центра экрана
            if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
                if (hit.collider.TryGetComponent(out DollHealth targetHealth))
                    if (targetHealth)
                        targetHealth.TakeDamage(damage, _playerOwner);
        }
    }
}