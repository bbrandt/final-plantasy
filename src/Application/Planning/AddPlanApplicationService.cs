using FluentValidation;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Application.Abstractions.Validations;
using TRS.FinalPlantasy.Application.Validations;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Domain.Planning;

namespace TRS.FinalPlantasy.Application.Planning;

internal class AddPlanApplicationService : IAddPlanApplicationService
{
    private readonly IValidator<PlanEntryModel> _validator;
    private readonly IPlanEntryUnitOfWorkFactory _unitOfWorkFactory;
    private readonly PlanningDomainService _domainService;

    public AddPlanApplicationService(
        IValidator<PlanEntryModel> validator,
        IPlanEntryUnitOfWorkFactory unitOfWorkFactory,
        PlanningDomainService domainService)
    {
        _validator = validator;
        _unitOfWorkFactory = unitOfWorkFactory;
        _domainService = domainService;
    }

    public async Task<ResultResponse<int?>> AddAsync(PlanEntryModel model, CancellationToken cancellationToken)
    {
        var response = (await _validator.ValidateAsync(model, cancellationToken)).ToValidationResponse<int?>(null);

        if (response.HasErrors())
        {
            return response;
        }

        /*
         * Validator has ensured these values are not null
         */
        var entity = _domainService.CreatePlanEntry(
            model.PlanType!.Value, 
            model.EventDate!.Value, 
            model.Amount!.Value, 
            model.RepeatOn!.Value,
            model.Description!);

        return await PersistEntityAsync(entity, cancellationToken);
    }

    private async Task<ResultResponse<int?>> PersistEntityAsync(
        PlanEntry entity,
        CancellationToken cancellationToken)
    {
        using var unitOfWork = await _unitOfWorkFactory.CreateAsync(cancellationToken);

        await unitOfWork.PlanEntries.AddAsync(entity, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        return new ResultResponse<int?>(entity.Id);
    }
}
