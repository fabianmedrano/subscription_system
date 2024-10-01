using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Models;
using System.Data;
namespace subscription_system.Services {
    public class FeatureService : IFeatureService {

        private ApplicationDbContext _context;
        private readonly ILogger _logger;
        public FeatureService(ApplicationDbContext applicationDbContext, ILogger<FeatureService> logger) {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _logger = logger;
        }

        //public async  Task<List<Feature>> GetFeatures()=>  await  ExecuteAsync(() => _context.Feature.ToListAsync());
        public async Task<List<Feature>> GetFeaturesAsync() {
            try {
                return await _context.Feature.ToListAsync();
            } catch (SqlException ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }

        }

        public async Task<Feature> GetFeatureAsync(int id) {
            try {
                // throw new Exception("Error simulado");
                return await _context.Feature.FindAsync(id) ?? new Feature();
            } catch (SqlException ex) {
                _logger.LogError(ex, "Database update error: {Message}", ex.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> AddFeatureAsync(Feature feature) {
            try {
                // throw new Exception("Error simulado");
                _context.Feature.Add(feature);
                return (await _context.SaveChangesAsync() > 0);

            } catch (DbUpdateException ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdateFeatureAsync(Feature feature) {
            try {
                _context.Update(feature);
                return (await _context.SaveChangesAsync() > 0);
            } catch (DbUpdateException dbEx) {
                _logger.LogError(dbEx, "Database update error: {Message}", dbEx.Message);
                throw;

            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> RemoveFeatureAsync(Feature feature) {
            try {
                // TODO: hay que mostrar cuales son los planes que tienen asociados  esta caracteristica
                // TODO: talves en el listado de planes se pueda buscar tambien por caracteristicas

                var pf = await _context.PlanFeature.AnyAsync(pf => pf.FeatureId == feature.Id);
                if (!pf) {
                    _context.Feature.Remove(feature);
                    return (await _context.SaveChangesAsync() > 0);
                } else {
                    throw new Exception("Existen planes con esta caracteristica asociada");
                }

            } catch (DbUpdateException dbEx) {
                _logger.LogError(dbEx, "Database update error: {Message}", dbEx.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }


        public async Task<bool> SaveChangesAsync() {
            try {
                return (await _context.SaveChangesAsync() > 0);
            } catch (DbUpdateException dbEx) {
                _logger.LogError(dbEx, "Database update error: {Message}", dbEx.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }

        public bool FeatureExists(int id) {
            try {
                return _context.Feature.Any(e => e.Id == id);
            } catch (SqlException ex) {
                _logger.LogError(ex, "Database update error: {Message}", ex.Message);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }

    }
}
