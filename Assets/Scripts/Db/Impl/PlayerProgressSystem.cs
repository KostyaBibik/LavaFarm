using UnityEngine;
using Zenject;

namespace Db.Impl
{
    public class PlayerProgressSystem : IPlayerProgressSystem, IInitializable
    {
        private int _countCarrots;

        public int CountCarrots => _countCarrots;

        [Inject] private IPrefsManager _prefsManager;

        private const string CarrotKey = "CountOfCarrots";
        
        private void InitProgressValues()
        {
            _countCarrots = _prefsManager.GetValue<int>(CarrotKey);
            Debug.Log(CountCarrots);
            Debug.Log($"carrots key {_prefsManager.HasKey(CarrotKey)}");
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