using NSubstitute;
using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Sessions.Events;
using Workshopper.Tests.Common.Sessions;

namespace Workshopper.Domain.Tests.Unit.Sessions;

public class SessionTests
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public SessionTests()
    {
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
    }

    [Fact]
    public void Cancel_ShouldThrowDomainException_WhenSessionAlreadyCanceled()
    {
        var onlineSession = SessionFactory.CreateOnlineSession();
        onlineSession.Cancel(_dateTimeProvider);

        var action = () => onlineSession.Cancel(_dateTimeProvider);

        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(SessionErrors.SessionAlreadyCanceled);
    }

    [Fact]
    public void Cancel_ShouldThrowDomainException_WhenSessionAlreadyStarted()
    {
        _dateTimeProvider.Now.Returns(new DateTimeOffset(2027, 5, 1, 10, 0, 0, TimeSpan.Zero));
        var onlineSession = SessionFactory.CreateOnlineSession(
            startDateTime: _dateTimeProvider.Now.AddHours(-1),
            endDateTime: _dateTimeProvider.Now.AddHours(1));

        var action = () => onlineSession.Cancel(_dateTimeProvider);

        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(SessionErrors.SessionAlreadyStarted);
    }

    [Fact]
    public void Cancel_ShouldMarkSessionAsCanceled_WhenSessionIsCancelable()
    {
        var onlineSession = SessionFactory.CreateOnlineSession();

        var action = () => onlineSession.Cancel(_dateTimeProvider);

        action.Should().NotThrow<DomainException>();
        onlineSession.IsCanceled.Should().BeTrue();
    }

    [Fact]
    public void Cancel_ShouldRaiseSessionCanceledDomainEvent_WhenSessionIsCancelable()
    {
        var onlineSession = SessionFactory.CreateOnlineSession();

        var action = () => onlineSession.Cancel(_dateTimeProvider);

        action.Should().NotThrow<DomainException>();
        onlineSession.GetDomainEvents.Should().ContainEquivalentOf(new SessionCanceledDomainEvent(onlineSession.Id));
    }
}