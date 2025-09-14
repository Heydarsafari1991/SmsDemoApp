using FluentValidation;
using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Features.SMS.Command;


public record ProcessPreSMSCommand(
    long Id ,
    int CustomerId,
    string Text,
    string ReciverPhoneNumber) : IRequest<OperationResult<bool>>, IValidatableModel<ProcessPreSMSCommand>
{
    public IValidator<ProcessPreSMSCommand> Validate(ValidationModelBase<ProcessPreSMSCommand> validator)
    {
       


        return validator;
    }
}