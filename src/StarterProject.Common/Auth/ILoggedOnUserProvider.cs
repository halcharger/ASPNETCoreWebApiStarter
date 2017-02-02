namespace StarterProject.Common.Auth
{
    public interface ILoggedOnUserProvider
    {
        string UserId { get; }
        string Username { get; }
        string Email { get; }
    }
}