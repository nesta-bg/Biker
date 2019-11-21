using System.Threading.Tasks;

namespace Biker.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikerDbContext context;

        public UnitOfWork(BikerDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
