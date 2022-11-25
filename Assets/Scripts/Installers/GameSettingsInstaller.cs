using Game.FarmLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private FarmGameParameters farmGameParameters;
        
        public override void InstallBindings()
        {
            Container.BindInstance(farmGameParameters);
        }
    }
}