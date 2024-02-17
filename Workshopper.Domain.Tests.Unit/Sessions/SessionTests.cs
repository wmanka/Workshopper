using FluentAssertions;
using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions;
using Workshopper.Tests.Common.Sessions;

namespace Workshopper.Domain.Tests.Unit.Sessions;

public class SessionTests
{
    [Fact]
    public void Cancel_ShouldThrowDomainException_WhenSessionAlreadyCanceled()
    {
        var onlineSession = SessionFactory.CreateOnlineSession();
        onlineSession.Cancel();

        var action = () => onlineSession.Cancel();

        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(SessionErrors.SessionAlreadyCanceled);
    }

    [Fact]
    public void Cancel_ShouldThrowDomainException_WhenSessionAlreadyStarted()
    {
        var onlineSession = SessionFactory.CreateOnlineSession(
            startDateTime: DateTimeOffset.Now.AddHours(-1),
            endDateTime: DateTimeOffset.Now.AddHours(1));

        var action = () => onlineSession.Cancel();

        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(SessionErrors.SessionAlreadyStarted);
    }

    [Fact]
    public void Cancel_ShouldCancelSession_WhenCancelIsPossible()
    {
        var onlineSession = SessionFactory.CreateOnlineSession();

        var action = () => onlineSession.Cancel();

        action.Should().NotThrow<DomainException>();
        onlineSession.IsCanceled.Should().BeTrue();
    }

    [Fact]
    public void Cancel_ShouldAddSessionCanceledDomainEvent()
    {
    }
}