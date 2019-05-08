using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.BoilerplateDemo.EntityFrameworkCore.Seed.Host
{
    public class InitialListAndListItemsDbBuilder
    {
        private readonly BoilerplateDemoDbContext _context;

        public InitialListAndListItemsDbBuilder(BoilerplateDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultListAndListItemsCreator(_context).Create();
        }
    }
}
