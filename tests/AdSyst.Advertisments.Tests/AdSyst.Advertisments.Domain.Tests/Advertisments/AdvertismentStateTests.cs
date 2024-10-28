using ErrorOr;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates;

namespace AdSyst.Advertisments.Domain.Tests.Advertisments
{
    public class AdvertismentStateTests
    {
        #region Archive tests

        [Theory]
        [MemberData(nameof(Archive_Should_ChangeState_InitialState))]
        public void Archive_Should_ChangeState(IAdvertismentState initialState)
        {
            ChangeStatusMustBeInNewState(
                initialState,
                state => state.Archive(),
                AdvertismentStatus.Archival
            );
        }

        public static IEnumerable<object[]> Archive_Should_ChangeState_InitialState =>
            new List<object[]>
            {
                new[] { new ActiveState() },
                new[] { new ModerationState() },
                new[] { new RejectedState() },
            };

        [Fact]
        public void Archive_When_IncorrectState_Should_ReturnError() =>
            ChangeStatusMustReturnError(
                new ArchivalState(),
                state => state.Archive(),
                AdvertismentErrors.IncorrectStateChangeError(AdvertismentStatus.Archival)
            );

        #endregion

        #region Restore tests

        [Fact]
        public void Restore_Should_ChangeState() =>
            ChangeStatusMustBeInNewState(
                new ArchivalState(),
                state => state.Restore(),
                AdvertismentStatus.Moderation
            );

        [Theory]
        [MemberData(nameof(Restore_When_IncorrectState_Should_ReturnError_InitialState))]
        public void Restore_When_IncorrectState_Should_ReturnError(
            IAdvertismentState initialState
        ) =>
            ChangeStatusMustReturnError(
                initialState,
                state => state.Restore(),
                AdvertismentErrors.IncorrectStateChangeError(initialState.Status)
            );

        public static IEnumerable<object[]> Restore_When_IncorrectState_Should_ReturnError_InitialState =>
            new List<object[]>
            {
                new[] { new ActiveState() },
                new[] { new ModerationState() },
                new[] { new RejectedState() },
            };

        #endregion

        #region Reject tests

        [Theory]
        [MemberData(nameof(Reject_Should_ChangeState_InitialState))]
        public void Reject_Should_ChangeState(IAdvertismentState initialState) =>
            ChangeStatusMustBeInNewState(
                initialState,
                state => state.Reject(),
                AdvertismentStatus.Rejected
            );

        public static IEnumerable<object[]> Reject_Should_ChangeState_InitialState =>
            new List<object[]> { new[] { new ModerationState() }, new[] { new ActiveState() }, };

        [Theory]
        [MemberData(nameof(Reject_When_IncorrectState_Should_ReturnError_InitialState))]
        public void Reject_When_IncorrectState_Should_ReturnError(
            IAdvertismentState initialState
        ) =>
            ChangeStatusMustReturnError(
                initialState,
                state => state.Reject(),
                AdvertismentErrors.IncorrectStateChangeError(initialState.Status)
            );

        public static IEnumerable<object[]> Reject_When_IncorrectState_Should_ReturnError_InitialState =>
            new List<object[]> { new[] { new ArchivalState() }, new[] { new RejectedState() }, };

        #endregion

        #region Approve tests

        [Fact]
        public void Approve_Should_ChangeState() =>
            ChangeStatusMustBeInNewState(
                new ModerationState(),
                state => state.Approve(),
                AdvertismentStatus.Active
            );

        [Theory]
        [MemberData(nameof(Reject_When_IncorrectState_Should_ReturnError_InitialState))]
        public void Approve_When_IncorrectState_Should_ReturnError(
            IAdvertismentState initialState
        ) =>
            ChangeStatusMustReturnError(
                initialState,
                state => state.Approve(),
                AdvertismentErrors.IncorrectStateChangeError(initialState.Status)
            );

        public static IEnumerable<object[]> Approve_When_IncorrectState_Should_ReturnError_InitialState =>
            new List<object[]>
            {
                new[] { new ActiveState() },
                new[] { new ArchivalState() },
                new[] { new RejectedState() },
            };

        #endregion

        #region Update tests

        [Theory]
        [MemberData(nameof(Update_Should_ChangeState_Data))]
        public void Update_Should_ChangeState(
            IAdvertismentState initialState,
            AdvertismentStatus expectedStatus
        ) => ChangeStatusMustBeInNewState(initialState, state => state.Update(), expectedStatus);

        public static IEnumerable<object[]> Update_Should_ChangeState_Data =>
            new List<object[]>
            {
                new object[] { new ActiveState(), AdvertismentStatus.Moderation },
                new object[] { new ArchivalState(), AdvertismentStatus.Archival },
                new object[] { new RejectedState(), AdvertismentStatus.Moderation },
                new object[] { new ModerationState(), AdvertismentStatus.Moderation },
            };

        #endregion

        #region Helper methods

        private static void ChangeStatusMustBeInNewState(
            IAdvertismentState currentState,
            Func<IAdvertismentState, ErrorOr<IAdvertismentState>> changeStateFunc,
            AdvertismentStatus exceptedNewStatus
        )
        {
            // Act
            var newState = changeStateFunc(currentState);

            // Assert
            newState.IsError.ShouldBeFalse();
            newState.Value.Status.ShouldBe(exceptedNewStatus);
        }

        private static void ChangeStatusMustReturnError(
            IAdvertismentState currentState,
            Func<IAdvertismentState, ErrorOr<IAdvertismentState>> changeStateFunc,
            Error error
        )
        {
            // Act
            var result = changeStateFunc(currentState);

            // Assert
            result.IsError.ShouldBeTrue();
            result.FirstError.Code.ShouldBe(error.Code);
            result.FirstError.Description.ShouldBe(error.Description);
        }

        #endregion
    }
}
