using FluentValidation;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Application.Abstractions.Validations;
using TRS.FinalPlantasy.Application.Validations;
using TRS.FinalPlantasy.Domain.Abstractions.Planning;

namespace TRS.FinalPlantasy.Application.Planning;

internal class UpdatePlanApplicationService : IUpdatePlanApplicationService
{
    private readonly IValidator<PlanEntryModel> _validator;
    private readonly IPlanEntryUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IPlanEntryDomainService _domainService;

    public UpdatePlanApplicationService(
        IValidator<PlanEntryModel> validator,
        IPlanEntryUnitOfWorkFactory unitOfWorkFactory,
        IPlanEntryDomainService domainService)
    {
        _validator = validator;
        _unitOfWorkFactory = unitOfWorkFactory;
        _domainService = domainService;
    }

    public async Task<ResultResponse<int?>> UpdateAsync(PlanEntryModel model, CancellationToken cancellationToken)
    {
        var response = (await _validator.ValidateAsync(model, cancellationToken)).ToValidationResponse<int?>(null);

        if (response.HasErrors())
        {
            return response;
        }

        using var unitOfWork = await _unitOfWorkFactory.CreateAsync(cancellationToken);

        var entity = await unitOfWork.PlanEntries.FindAsync(model.Id!.Value, cancellationToken);

        if (entity == null)
        {
            throw new NullReferenceException($"Unable to find plan entry for id {model.Id}");
        }

        _domainService.UpdatePlanEntry(
            model.PlanType!.Value,
            model.EventDate!.Value,
            model.Amount!.Value,
            model.RepeatOn!.Value,
            model.Description!,
            model.EndDate,
            entity);

        await unitOfWork.CompleteAsync(cancellationToken);

        return new ResultResponse<int?>(entity.Id);
    }
}
