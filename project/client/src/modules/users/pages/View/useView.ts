// packages
import { useQuery, useMutation } from "react-query";
import { toast } from "react-toastify";
import { useNavigate, useParams } from "react-router-dom";

// source
import { 
    activateUser, 
    deactivateUser, 
    deleteUser, 
    generatePasswordUser, 
    getUser, 
    lockUser, 
    unlockUser 
} from "src/modules/users/api/api";

const useView = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    // Fetch user details
    const { data: user, isError, error, isLoading } = useQuery(
        ['user', id],
        () => getUser(id!),
        {
            enabled: !!id,
            onError: (err: any) => {
                console.error("Error fetching user:", err);
                toast.error("Failed to fetch user.");
            },
        }
    );

    // Generic mutation function
    const useActionMutation = (action: (id: string) => Promise<any>, successMessage: string, reload = false) =>
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

    const deleteMutation = useActionMutation(deleteUser, "User deleted successfully");
    const activateMutation = useActionMutation(activateUser, "User activated successfully", true);
    const deactivateMutation = useActionMutation(deactivateUser, "User deactivated successfully", true);
    const lockMutation = useActionMutation(lockUser, "User locked successfully", true);
    const unlockMutation = useActionMutation(unlockUser, "User unlocked successfully", true);
    const generatePasswordMutation = useActionMutation(generatePasswordUser, "Password generated successfully", true);

    return {
        handleDelete: () => deleteMutation.mutate(id!),
        handleActivateUser: () => activateMutation.mutate(id!),
        handleDeactivateUser: () => deactivateMutation.mutate(id!),
        handleLockUser: () => lockMutation.mutate(id!),
        handleUnlockUser: () => unlockMutation.mutate(id!),
        handleGeneratePasswordUser: () => generatePasswordMutation.mutate(id!),
        user: user?.data,
        isError,
        error,
        isLoading,
        navigate,
        id,
    };
};

export default useView;
