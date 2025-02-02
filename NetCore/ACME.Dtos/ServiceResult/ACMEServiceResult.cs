namespace ACME.Dtos
{
    using FluentValidation.Results;
    public class ACMEServiceResult<T, TId> where T : ACMEDto<TId>
    {
        public T ResultObject { get; set; }
        public ValidationResult Validation { get; set; }
    }
}
