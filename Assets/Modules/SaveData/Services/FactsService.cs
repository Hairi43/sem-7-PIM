using System.Collections.Generic;
using Modules.SaveData.Model;


namespace Modules.SaveData.Services {
    public class FactsService : CollectibleService {
        protected override List<CollectibleState> DataList => SaveManager.gameData.facts;
    }
}