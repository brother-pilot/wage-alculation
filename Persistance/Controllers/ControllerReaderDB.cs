using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance.Controllers
{
    public class ControllerReaderDB : IControllerData
    {
        public List<T> ReadData<T>() where T : class
        {
            using (var db = new Context())
            {
                var result = db.Set<T>().Where(t => true).ToList();
                return result;
            }
        }

        public bool WriteData<T>(List<T> item) where T : class
        {
            try
            {
                using (var db = new Context())
                {
                    db.Set<T>().AddRange(item);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
