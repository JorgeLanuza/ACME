namespace ACME.Dtos
{
    using FluentValidation.Results;
    public class ACMECollectionServiceResult<T, TId> where T : ACMEDto<TId>
    {
        public DtoCollectionResult<T, TId>? ResultObject { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
