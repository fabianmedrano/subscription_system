using subscription_system.Models;

namespace subscription_system.Services
{
    public interface IFeatureService 
    {
        Task<List<Feature>> GetFeaturesAsync();

        Task<Feature> GetFeatureAsync(int id);

        Task<bool> AddFeatureAsync(Feature feature);

        Task<bool> UpdateFeatureAsync(Feature fetaure);
      //  void DeleteFeature(int id);
        Task<bool> RemoveFeatureAsync(Feature feature);
 
        Task<bool> SaveChangesAsync();
        bool FeatureExists(int id);
    }
}
