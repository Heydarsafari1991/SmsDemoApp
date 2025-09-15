using System.IdentityModel.Tokens.Jwt;

using FluentAssertions;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Adly.Identity.Tests;

public class IdentityTests:IClassFixture<IdentityTestSetup>
{
    private IServiceProvider _serviceProvider;

    public IdentityTests(IdentityTestSetup setup)
    {
        _serviceProvider = setup.ServiceProvider;
    }


    
    
    

}