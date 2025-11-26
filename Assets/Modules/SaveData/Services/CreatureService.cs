using System.Collections.Generic;
using Modules.SaveData.Model;

namespace Modules.SaveData.Services {
    namespace Modules.SaveData.Services {
        public class CreatureService : CollectibleService {
            protected override List<CollectibleState> DataList => SaveManager.gameData.creatures;
        }
    }
}