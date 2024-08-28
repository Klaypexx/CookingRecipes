using Application.ResultObject;

namespace Application.Validation;

public interface IValidator<T>
{
    Result Validate( T entity );
}
