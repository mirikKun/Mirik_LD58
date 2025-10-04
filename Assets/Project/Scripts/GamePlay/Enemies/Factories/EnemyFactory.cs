using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.Factories
{
    public class EnemyFactory:IEnemyFactory
    {
        private DiContainer _container;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(DiContainer container,IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _container = container;
        }
    }
}