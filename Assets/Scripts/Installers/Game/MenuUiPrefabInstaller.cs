using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(MenuUiPrefabInstaller), fileName = nameof(MenuUiPrefabInstaller))]
    public class MenuUiPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        //[SerializeField] private MenuView _menuView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
            var canvasTransform = canvasView.transform;
            
        }
    }
}