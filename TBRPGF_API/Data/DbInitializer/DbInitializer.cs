using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRPGF_API.Data.Context;

namespace TBRPGF_API.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly TBRPGDBContext _db;

        public DbInitializer(TBRPGDBContext db)
        {
            _db = db;
        }


        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            return;
        }
    }
}
