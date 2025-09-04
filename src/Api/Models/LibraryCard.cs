// This should really be a signed JWT certifacate
public class LibraryCard
{
    public Guid Id;

    public LibraryCard(PasswordIdentifier identifier)
    {
        // TODO! What if the ID alread has a Library Card registered
        Id = Guid.NewGuid();
    }
}
