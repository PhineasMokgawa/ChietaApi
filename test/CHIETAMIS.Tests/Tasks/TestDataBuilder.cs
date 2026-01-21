using CHIETAMIS.EntityFrameworkCore;
using CHIETAMIS.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Tests
{
    public class TestDataBuilder
    {
        private readonly CHIETAMISDbContext _context;

        public TestDataBuilder(CHIETAMISDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.Tasks.AddRange(
                new CHIETAMIS.Tasks.Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality."),
                new CHIETAMIS.Tasks.Task("Clean your room") { State = TaskState.Completed }
                );
        }
    }
}
