namespace TesisApi.Domain.Exceptions;

/// <summary>Recurso no encontrado (404).</summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

/// <summary>Regla de negocio violada, p. ej. duplicados o límite de tesis (409).</summary>
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}

/// <summary>Dato de entrada inválido (400).</summary>
public class ValidationAppException : Exception
{
    public ValidationAppException(string message) : base(message) { }
}
