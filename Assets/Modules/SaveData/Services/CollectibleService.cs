using System.Collections.Generic;
using System.Linq;
using Modules.SaveData.Model;

namespace Modules.SaveData.Services {
    public abstract class CollectibleService : ICollectibleService {
        protected abstract List<CollectibleState> DataList { get; }

        public CollectibleState GetCollectibleById(string nameId) {
            return DataList.FirstOrDefault(x => x.nameId == nameId);
        }

        public bool IsCollected(string nameId) {
            return GetCollectibleById(nameId)?.isCollected ?? false;
        }

        public void Collect(string nameId) {
            var collectible = GetCollectibleById(nameId);
            if (collectible == null) {
                DataList.Add(new CollectibleState { nameId = nameId, isCollected = true });
            } else {
                collectible.isCollected = true;
            }
        }

        public bool UnCollect(string nameId) {
            var collectible = GetCollectibleById(nameId);
            if (collectible == null) return false;

            collectible.isCollected = false;
            return true;
        }
    }
}