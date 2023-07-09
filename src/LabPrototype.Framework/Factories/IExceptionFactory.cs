namespace LabPrototype.Framework.Factories
{
    public interface IExceptionFactory<TException, in TContext> where TException : Exception
    {
        TException GetException(TContext context);
    }
}
