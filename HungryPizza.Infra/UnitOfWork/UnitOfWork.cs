using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.Repositories;
using HungryPizza.Infra.Repositories.Interface;
using HungryPizza.Infra.UnitOfWork.Interface;

namespace HungryPizza.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        private IProductOrderRepository _productOrderRepository;
        private IPizzaFlavorRepository _pizzaFlavorRepository;
        private IProductRepository _productRepository;

        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_session)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_session)); }
        }

        public IProductOrderRepository ProductOrderRepository
        {
            get { return _productOrderRepository ?? (_productOrderRepository = new ProductOrderRepository(_session)); }
        }

        public IPizzaFlavorRepository PizzaFlavorRepository
        {
            get { return _pizzaFlavorRepository ?? (_pizzaFlavorRepository = new PizzaFlavorRepository(_session)); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_session)); }
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
