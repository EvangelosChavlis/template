// packages
import { useQuery, useMutation } from "react-query";
import { toast } from "react-toastify";
import { useNavigate, useParams } from "react-router-dom";

// source
import { 
    activateUser, 
    assingRoleToUser, 
    deactivateUser, 
    deleteUser, 
    generatePasswordUser, 
    getUser, 
    lockUser, 
    unassingRoleFromUser, 
    unlockUser 
} from "src/modules/users/api/api";
import { ItemRoleDto } from "src/models/auth/roleDto";
import { getRoles } from "src/modules/roles/api/api";

const useView = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    // Fetch user details
    const { data: user, isError, error, isLoading } = useQuery(
        ['user', id],
        () => getUser(id!),
        {
            enabled: !!id,
            suspense: true,
            onError: (err: any) => {
                console.error("Error fetching user:", err);
                toast.error("Failed to fetch user.");
            },
        }
    );

    const { data: roles} = useQuery<ItemRoleDto[], Error>(
        "roles",
        getRoles,
        {
          suspense: true,
          onError: (error) => {
            console.error("Error fetching roles:", error);
            toast.error("Failed to fetch roles.");
          },
        }
      );

    // Generic mutation function
    const useActionMutation = (action: (id: string) => Promise<any>, _successMessage: string, reload = false) =>
        useMutation(action, {
            onSuccess: (result) => {
                toast.success(result.data);
                if (reload) {
                    setTimeout(() => window.location.reload(), 2000);
                } else {
                    setTimeout(() => navigate("/users"), 2000);
                }
            },
            onError: (err) => {
                console.error(`Error during action:`, err);
                toast.error(`An error occurred while performing the action.`);
            },
        });


    const useActionMutationRefToRole = (
        action: (userId: string, roleId: string) => Promise<any>,  // Accepts an action with two parameters
        _successMessage: string, 
        reload = false
    ) =>
        useMutation((variables: { userId: string, roleId: string }) => action(variables.userId, variables.roleId), {
            onSuccess: (result) => {
                toast.success(result.data);
                if (reload) {
                    setTimeout(() => window.location.reload(), 2000);
                } else {
                    setTimeout(() => navigate("/users"), 2000);
                }
            },
            onError: (err) => {
                console.error(`Error during action:`, err);
                toast.error(`An error occurred while performing the action.`);
            },
        });

    const deleteMutation = useActionMutation(deleteUser, "User deleted successfully");
    const activateMutation = useActionMutation(activateUser, "User activated successfully", true);
    const deactivateMutation = useActionMutation(deactivateUser, "User deactivated successfully", true);
    const lockMutation = useActionMutation(lockUser, "User locked successfully", true);
    const unlockMutation = useActionMutation(unlockUser, "User unlocked successfully", true);
    const generatePasswordMutation = useActionMutation(generatePasswordUser, "Password generated successfully", true);
    const assignRoleToUserMutation = useActionMutationRefToRole(assingRoleToUser, "Role assigned to user successfully", true);
    const unassignRoleToUserMutation = useActionMutationRefToRole(unassingRoleFromUser, "Role unassigned from user successfully", true);

    return {
        handleDelete: () => deleteMutation.mutate(id!),
        handleActivateUser: () => activateMutation.mutate(id!),
        handleDeactivateUser: () => deactivateMutation.mutate(id!),
        handleLockUser: () => lockMutation.mutate(id!),
        handleUnlockUser: () => unlockMutation.mutate(id!),
        handleGeneratePasswordUser: () => generatePasswordMutation.mutate(id!),
        handleAssignRoleToUser: (userId: string, roleId: string) => assignRoleToUserMutation.mutate({ userId, roleId }),
        handleUnassignRoleFromUser: (userId: string, roleId: string) => unassignRoleToUserMutation.mutate({ userId, roleId }),
        user: user?.data!,
        roles: roles || [],
        isError,
        error,
        isLoading,
        navigate,
        id,
    };
};

export default useView;
