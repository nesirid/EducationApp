

namespace Service.Helpers.Exceptions
{
    internal class NotFoundException:Exception
    {
        public NotFoundException(string msj) : base(msj) { }
    }
}
