using HungryPizza.Infra.Repositories.Interface;

namespace HungryPizza.Infra.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        IUserRepository UserRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
