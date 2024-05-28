using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Models;
using System.Data;

namespace subscription_system.Services
{
    public class FeatureService :  IFeatureService
    {
        
        public ApplicationDbContext _context;

        public FeatureService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
 



        public async  Task<List<Feature>> GetFeatures() {
           return await _context.Feature.ToListAsync();
        }


        public async Task<Feature> GetFeature(int id){

          return await  _context.Feature.FindAsync(id);
        }

        public async  void AddFeature(Feature feature) {
            _context.Feature.Add(feature);
           await _context.SaveChangesAsync();
        }
        public async void Update(Feature feature) {
            _context.Update(feature);
         await  _context.SaveChangesAsync();
        
        }

        public  void Delete(int id)
        {
            Feature feature = _context.Feature.Find(id);
            if(feature != null)
             _context.Feature.Remove(feature);
        }
        public void Remove(Feature feature) {
            _context.Feature.Remove(feature);
        }
        public bool ContextIsNull() {
            return (_context.Feature == null);
        }
        public bool FeatureExists(int id)
        {

            return (_context.Feature?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<int> SaveChangesAsync()
        {
           return  await _context.SaveChangesAsync();
        }
       
    }
}
