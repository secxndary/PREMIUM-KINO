using System;
using PREMIUM_KINO.EFCore;



namespace PREMIUM_KINO.Classes
{
    public class UnitOfWork : IDisposable
    {
        private DBContext context = new DBContext();
        private bool disposed = false;

        private ScheduleRepo scheduleRepo;
        private MovieRepo movieRepo;
        private OrdersRepo ordersRepo;
        private UsersRepo usersRepo;



        public ScheduleRepo ScheduleRepo
        {
            get
            {
                if (scheduleRepo == null)
                    scheduleRepo = new ScheduleRepo();
                return scheduleRepo;
            }
        }


        public MovieRepo MovieRepo
        {
            get
            {
                if (movieRepo == null)
                    movieRepo = new MovieRepo();
                return movieRepo;
            }
        }


        public OrdersRepo OrdersRepo
        {
            get
            {
                if (ordersRepo == null)
                    ordersRepo = new OrdersRepo();
                return ordersRepo;
            }
        }


        public UsersRepo UsersRepo
        {
            get
            {
                if (usersRepo == null)
                    usersRepo = new UsersRepo();
                return usersRepo;
            }
        }




        public void Save() => context.SaveChanges();


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
                this.disposed = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
