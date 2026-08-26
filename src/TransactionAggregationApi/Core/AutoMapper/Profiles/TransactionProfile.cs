using AutoMapper;
using TransactionAggregationApi.Application.Domain.Entities;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Core.AutoMapper.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionEntity, TransactionResponse>();
        CreateMap<AggregationSummaryEntity, AggregationSummaryResponse>();
        CreateMap<ClientAggregationEntity, ClientAggregationResponse>();
        CreateMap<CategoryBreakdownEntity, CategoryBreakdownResponse>();
        CreateMap<SourceBreakdownEntity, SourceBreakdownResponse>();
        CreateMap<MonthlyAggregationEntity, MonthlyAggregationResponse>();
        CreateMap<TopSpenderEntity, TopSpenderResponse>();
    }
}
