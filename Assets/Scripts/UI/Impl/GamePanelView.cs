using Db;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Impl
{
    public class GamePanelView : UiPanel, IInitializable
    {
        [SerializeField] private TMP_Text carrotsCounter;
        [SerializeField] private TMP_Text experienceCounter;

        private IPrefsManager _prefsManager;

        private const string CarrotsPrefixLabel = "Carrots: ";
        private const string ExperiencePrefixLabel = "Experience: ";
        
        public void Initialize()
        {
            var carrotsKey = PlayerPrefKeys.CarrotKey;
            var expKey = PlayerPrefKeys.Experience;
            
            var carrotsValue = _prefsManager.GetValue<int>(carrotsKey);
            var expValue = _prefsManager.GetValue<int>(expKey);
            
            SetCarrotsCount(carrotsValue);
            SetExperienceCount(expValue);
            
            _prefsManager.HasUpdateValue += OnUpdateCounter;
        }

        [Inject]
        public void Construct(IPrefsManager prefsManager)
        {
            _prefsManager = prefsManager;
        }

        private void SetCarrotsCount(int value)
        {
            carrotsCounter.text = $"{CarrotsPrefixLabel} {value.ToString()}";
        }
        
        private void SetExperienceCount(int value)
        {
            experienceCounter.text = $"{ExperiencePrefixLabel} {value.ToString()}";
        }

        private void OnUpdateCounter(int newValue, string key)
        {
            if (key == PlayerPrefKeys.CarrotKey)
            {
                SetCarrotsCount(_prefsManager.GetValue<int>(key));
            }
            else if (key == PlayerPrefKeys.Experience)
            {
                SetExperienceCount(_prefsManager.GetValue<int>(key));
            }
        }
    }
}