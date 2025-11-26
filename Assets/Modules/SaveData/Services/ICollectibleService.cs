using Modules.SaveData.Model;

namespace Modules.SaveData.Services {
    public interface ICollectibleService {
        /// <summary>
        /// returns Collectible entry by nameId
        /// </summary>
        /// <param name="nameID"></param>
        /// <returns>CollectibleState</returns>
        public CollectibleState GetCollectibleById(string nameId);
        /// <summary>
        /// checks whether collectible is collected
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns>bool</returns>
        public bool IsCollected(string nameId);
        /// <summary>
        /// marks collectible as collected
        /// </summary>
        /// <param name="nameId"></param>
        public void Collect(string nameId);

        /// <summary>
        /// unmarks collectible as collected, returns true if success, false if item doesn't exist
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public bool UnCollect(string nameId);
    }
}