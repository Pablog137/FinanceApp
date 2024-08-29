namespace Finance.API.Exceptions
{
    public class ContactAlreadyExistsException : Exception
    {
        public ContactAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
