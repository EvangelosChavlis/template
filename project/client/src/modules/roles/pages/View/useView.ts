// packages
import { useQuery, useMutation } from 'react-query';
import { toast } from 'react-toastify';
import { useNavigate, useParams } from 'react-router-dom';

// source
import { ItemRoleDto } from 'src/models/auth/roleDto';
import { activateRole, deactivateRole, deleteRole, getRole } from 'src/modules/roles/api/api';
import { ItemResponse } from 'src/models/common/itemResponse';

const useView = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: result, isLoading, isError, error } = useQuery<ItemResponse<ItemRoleDto>, Error>(
    ['role', id],
    () => getRole(id!),
    { 
      enabled: !!id,
      suspense: true,
    }
  );

  const useActionMutation = (action: (id: string) => Promise<any>, _successMessage: string, reload = false) =>
    useMutation(action, {
        onSuccess: (result) => {
            toast.success(result.data);
            if (reload) {
                setTimeout(() => window.location.reload(), 2000);
            } else {
                setTimeout(() => navigate("/roles"), 2000);
            }
        },
        onError: (err) => {
            console.error(`Error during action:`, err);
            toast.error(`An error occurred while performing the action.`);
        },
    });

  const deleteMutation = useActionMutation(deleteRole, "Role deleted successfully");
  const activateMutation = useActionMutation(activateRole, "Role activated successfully", true);
  const deactivateMutation = useActionMutation(deactivateRole, "Role deactivated successfully", true);

  return {
    id,
    navigate,
    role: result?.data!,
    isLoading,
    isError,
    error,
    handleActivateRole: () => activateMutation.mutate(id!),
    handleDeactivateRole: () => deactivateMutation.mutate(id!),
    handleDelete: () => deleteMutation.mutate(id!),
  };
};

export default useView;
