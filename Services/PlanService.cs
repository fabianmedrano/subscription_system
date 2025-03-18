using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Models;

namespace subscription_system.Services {
    public class PlanService : IPlanService {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PlanService(ApplicationDbContext context,ILogger<PlanService> logger) {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
            _logger = logger;
        }


        // GET FUCNTIONS
        public async Task<List<Plan>> GetPlanListAsync() {
            try {
                return await _context.Plan.ToListAsync();
            } catch(SqlException ex) {
                _logger.LogError(ex, "{message}",ex.Message);
                throw;
            } catch (Exception ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            }
         
        }

        public async Task<Plan> FindPlanAsync(int id) {
            try {
                return await _context.Plan.FindAsync( id) ?? throw new ArgumentException("Plan not found"); 
            } catch (SqlException ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            } catch (Exception ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            }
        }

        //GET FEATURES

        public Task<List<Feature>> GetFeaturesListAsync() {
            return _context.Feature.ToListAsync();
        }

        //INSERT FUNCTIONS
        public async Task<bool> AddPlanAsync(Plan plan, List<int> featuresSelected) {
           
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try {
                await _context.Plan.AddAsync(plan);
                var result = await _context.SaveChangesAsync();
                if (result > 0) {

                    var planFeatures = featuresSelected.Select(featureId => new PlanFeature {
                        PlanId = plan.Id, 
                        FeatureId = featureId
                    });

                    await _context.PlanFeature.AddRangeAsync(planFeatures);// perece que solo inserto una de lascaracteristicas
                    var featuresResult = await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return featuresResult > 0;
                }
                await transaction.RollbackAsync();
                return false;
            } catch (DbUpdateException ex) {
                _logger.LogError(ex, "{message}", ex.Message);
                await transaction.RollbackAsync(); 
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "{message}", ex.Message);
                await transaction.RollbackAsync(); 
                throw;
            }
        }
        //UPDATE FUNCTIONS
        public async Task<bool> UpdatePlanAsync(Plan plan) {
            try {
                _context.Update(plan);
                return (await _context.SaveChangesAsync() > 0);
            } catch (DbUpdateException ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                 throw;
            } catch (Exception ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            }
        }

        //

        public bool CheckSubscriptionsExist(int planId) {
            try {
                return _context.Subscriptions.Any(p => p.Id == planId);
            } catch (SqlException ex) {
                _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            }
        }


        //DELETE

        public async Task<bool> RemovePlanAsync(Plan plan) {
            try {
                _context.Plan.Remove(plan);
                return (await _context.SaveChangesAsync() > 0);
            } catch (DbUpdateException ex) {
                _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            } catch (Exception ex) {
                 _logger.LogError(ex,"{message}" ,ex.Message);
                throw;
            }
        }


    }
}
