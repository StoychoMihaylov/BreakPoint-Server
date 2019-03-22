namespace BreakPoint.Data.EntityModels
{
    public class User
    {
        int Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        string Hash { get; set; }

        string Salt { get; set; }
    }
}
