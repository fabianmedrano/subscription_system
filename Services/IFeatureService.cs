using subscription_system.Models;

namespace subscription_system.Services
{
    public interface IFeatureService 
    {
        Task<List<Feature>> GetFeatures();

        Task<Feature> GetFeature(int id);

        void AddFeature(Feature feature);

        void Update(Feature fetaure);
        void Delete(int id);
        void Remove(Feature feature);
        bool ContextIsNull();
        Task<int> SaveChangesAsync();
        bool FeatureExists(int id);
    }
}
