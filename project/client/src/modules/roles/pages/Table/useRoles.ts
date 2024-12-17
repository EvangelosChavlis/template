// packages
import { useQuery } from "react-query";

// source
import { ItemRoleDto } from "src/models/auth/roleDto";
import { getRoles } from "src/modules/roles/api/api";

const useRoles = () => {
  const { data: roles, isLoading, isError, error } = useQuery<ItemRoleDto[], Error>(
    "roles",
    getRoles,
    {
      onError: (error) => {
        console.error("Error fetching roles:", error);
      },
    }
  );

  return {
    roles,
    isLoading,
    isError,
    error,
  };
};

export default useRoles;
