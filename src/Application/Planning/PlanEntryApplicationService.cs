using FluentValidation;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Application.Abstractions.Validations;
using TRS.FinalPlantasy.Application.Validations;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Domain.Planning;

namespace TRS.FinalPlantasy.Application.Planning;

internal class PlanEntryApplicationService : IPlanEntryApplicationService
{
    private readonly IValidator<PlanEntryModel> _validator;
    private readonly IPlanEntryUnitOfWorkFactory _unitOfWorkFactory;
    private readonly PlanEntryDomainService _domainService;

    public PlanEntryApplicationService(
        IValidator<PlanEntryModel> validator,
        IPlanEntryUnitOfWorkFactory unitOfWorkFactory,
        PlanEntryDomainService domainService)
    {
        _validator = validator;
        _unitOfWorkFactory = unitOfWorkFactory;
        _domainService = domainService;
    }

    public async Task<ResultResponse<int?>> AddPlanEntryAsync(PlanEntryModel model, CancellationToken cancellationToken)
    {
        var response = (await _validator.ValidateAsync(model, cancellationToken)).ToValidationResponse<int?>(null);

        if (response.HasErrors())
        {
            return response;
        }

        var entity = _domainService.Create(
            model.PlanType!.Value, 
            model.EventDate!.Value, 
            model.Amount!.Value, 
            model.RepeatOn);

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
