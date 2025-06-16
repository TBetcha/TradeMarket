public interface IJwtUtils
{
  string GenerateJwtToken(string email, string userId);
}
