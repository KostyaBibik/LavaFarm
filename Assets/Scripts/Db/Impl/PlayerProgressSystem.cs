using UnityEngine;
using Zenject;

namespace Db.Impl
{
    public class PlayerProgressSystem : IPlayerProgressSystem, IInitializable
    {
        private int _countCarrots;

        public int CountCarrots => _countCarrots;

        [Inject] private IPrefsManager _prefsManager;
        
        private void InitProgressValues()
        {
            var carrotsKey = PlayerPrefKeys.CarrotKey;
            _countCarrots = _prefsManager.GetValue<int>(carrotsKey);
        }
        
        public void Loq()
        {
            Debug.Log("PlayerProgressSystemLoq");
        }

        public void Initialize()
        {
            InitProgressValues();
        }
    }
}