namespace MvcWhatsUp.Repositories
{
    public class DummyUsersRepository : IUsersRepository
    {
        List<Models.User> users =
            [
                    new Models.User(1, "Jonathan", "063100606", "jonathan@gmail.com", "Passpasswordword"),
                    new Models.User(2, "Joseph", "0631228374", "joseph@hotmail.com", "Hermit4Purple"),
                    new Models.User(3, "Jotaro", "008523332", "jo_jo@gmail.com", "Qffghwwvgj"),
            ];

        public List<Models.User> GetAll()
        {
            return users;
        }

        public Models.User? GetById(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId);   
        }

        public void Add(Models.User user)
        {
            
        }

        public void Update(Models.User user)
        {
            
        }

        public void Delete(int userId)
        {
            
        }
    }
}
