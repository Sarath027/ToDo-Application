namespace ToDoApp.Repository.Interfaces
{
    public interface IUserRepositoryInterface
    {
        public bool ValidateUser(string username, string password);

        public Boolean SignUp(string username, string password);

        public int GetUserId(string username, string password);
    }
}
