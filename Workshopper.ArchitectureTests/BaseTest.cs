using System.Reflection;
using Workshopper.Api;
using Workshopper.Api.Auth;
using Workshopper.Api.Notifications;
using Workshopper.Api.Sessions;
using Workshopper.Application;
using Workshopper.Domain.Common;
using Workshopper.Infrastructure;

namespace Workshopper.ArchitectureTests;

public abstract class BaseTest
{
    protected readonly static Assembly DomainAssembly = typeof(DomainEntity).Assembly;
    protected readonly static Assembly ApplicationAssembly = typeof(ApplicationModule).Assembly;
    protected readonly static Assembly InfrastructureAssembly = typeof(InfrastructureModule).Assembly;
    protected readonly static Assembly ApiAssembly = typeof(ApiModule).Assembly;
    protected readonly static Assembly SessionsApiAssembly = typeof(SessionsModule).Assembly;
    protected readonly static Assembly AuthApiAssembly = typeof(AuthModule).Assembly;
    protected readonly static Assembly NotificationsApiAssembly = typeof(NotificationsModule).Assembly;

    protected static string[] DomainAssemblies =>
    [
        DomainAssembly.GetName().Name!
    ];

    protected static string[] ApplicationAssemblies =>
    [
        ApplicationAssembly.GetName().Name!
    ];

    protected static string[] InfrastructureAssemblies =>
    [
        InfrastructureAssembly.GetName().Name!
    ];

    protected static string[] PresentationAssemblies =>
    [
        ApiAssembly.GetName().Name!,
        AuthApiAssembly.GetName().Name!,
        SessionsApiAssembly.GetName().Name!,
        NotificationsApiAssembly.GetName().Name!
    ];
}