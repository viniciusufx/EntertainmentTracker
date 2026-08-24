using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Domain.Services
{
    public sealed class UserRoleChangePolicy
    {
        public bool CanAssignRole(
            UserRole actorRole,
            UserRole targetCurrentRole,
            UserRole targetNewRole)
        {
            if (actorRole == UserRole.Owner)
            {
                return CanOwnerAssignRole(
                    targetCurrentRole,
                    targetNewRole);
            }

            if (actorRole == UserRole.Administrator)
            {
                return CanAdministratorAssignRole(
                    targetCurrentRole,
                    targetNewRole);
            }

            if (actorRole == UserRole.Editor)
            {
                return CanEditorAssignRole(
                    targetCurrentRole,
                    targetNewRole);
            }

            if (actorRole == UserRole.Moderator)
            {
                return CanModeratorAssignRole(
                    targetCurrentRole,
                    targetNewRole);
            }

            return false;
        }

        private static bool CanOwnerAssignRole(
            UserRole targetCurrentRole,
            UserRole targetNewRole)
        {
            return IsRoleInRange(
                targetCurrentRole,
                UserRole.User,
                UserRole.Administrator)
                && IsRoleInRange(
                    targetNewRole,
                    UserRole.User,
                    UserRole.Administrator);
        }

        private static bool CanAdministratorAssignRole(
            UserRole targetCurrentRole,
            UserRole targetNewRole)
        {
            return IsRoleInRange(
                targetCurrentRole,
                UserRole.User,
                UserRole.Editor)
                && IsRoleInRange(
                    targetNewRole,
                    UserRole.User,
                    UserRole.Editor);
        }

        private static bool CanEditorAssignRole(
            UserRole targetCurrentRole,
            UserRole targetNewRole)
        {
            return IsRoleInRange(
                targetCurrentRole,
                UserRole.User,
                UserRole.Moderator)
                && IsRoleInRange(
                    targetNewRole,
                    UserRole.User,
                    UserRole.Moderator);
        }

        private static bool CanModeratorAssignRole(
            UserRole targetCurrentRole,
            UserRole targetNewRole)
        {
            return IsRoleInRange(
                targetCurrentRole,
                UserRole.User,
                UserRole.Contributor)
                && IsRoleInRange(
                    targetNewRole,
                    UserRole.User,
                    UserRole.Contributor);
        }

        private static bool IsRoleInRange(
            UserRole role,
            UserRole minimumRole,
            UserRole maximumRole)
        {
            return role >= minimumRole && role <= maximumRole;
        }
    }
}