using MoonHotels.Connector.Application.Pipelines.Steps;
using MoonHotels.Connector.Domain.Contracts.ErrorMessage;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Connector.Application.Pipelines;

public class SearchPipeline :
    IPipeline<SearchRequest, SearchResponse>
{
    private readonly
        IOperationStep<SearchRequest, SearchResponse>
        _operationsStep;

    public SearchPipeline(
        IOperationStep<SearchRequest, SearchResponse> operationsStep)
    {
        _operationsStep = operationsStep;
    }

    public async Task<SearchResponse> ProcessAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
    {
        var operationsResponse = await
            _operationsStep.DoOperations(searchRequest);

        if (operationsResponse.ErrorMessages.HaveErrors())
        {
            return BuildErrorResponse(operationsResponse.ErrorMessages);
        }

        var searchRs = operationsResponse.ConnectorResponse;

        return searchRs;
    }

    private SearchResponse BuildErrorResponse(IEnumerable<ErrorMessage> adviseMessages)
    {
        var adviseMessagesArray = adviseMessages as ErrorMessage[] ?? adviseMessages.ToArray();

        var rs = SearchResponse.BuildErrorResponse(adviseMessagesArray);

        return rs;
    }
}