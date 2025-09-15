using SmsDemoApp.Domain.Entities.Message;

namespace SmsDemoApp.Application.Features.SMS.Queries;

public record GetCustomerSMSsQueryResult(int TotalCount, List<CustomerSMS> CustomerSMSs);
public record CustomerSMS(Guid Id, int CustomerId, string Text, string ReciverPhoneNumber, MessageDelivaryStatus Status);

