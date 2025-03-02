namespace MvcWhatsUp.Repositories
{
    public interface IUsersRepository
    {
        List<Models.User> GetAll();
        Models.User? GetById(int userId);
        void Add(Models.User user);
        void Update(Models.User user);
        void Delete(Models.User user);
    }
}
