using subscription_system.Models;

namespace subscription_system.Services {
    public interface IPlanService {

        // GET FUNCTIONS
        Task<List<Plan>> GetPlanListAsync();
        Task<Plan> FindPlanAsync(int id);
        Task<List<Feature>> GetFeaturesListAsync();

        //INSERT FUNCTIONS
        Task<bool> AddPlanAsync(Plan plan);
        // UPDATE FUNCTIONS
        Task<bool> UpdatePlanAsync(Plan plan);
        //CHECK FUNCTIONS
        bool CheckSubscriptionsExist(int planId);
        //DELETE FUNCTIONS
        Task<bool> RemovePlanAsync(Plan plan);
    }
}
