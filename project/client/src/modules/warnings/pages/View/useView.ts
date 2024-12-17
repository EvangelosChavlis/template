// packages
import { useQuery, useMutation } from 'react-query';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';

// source
import { deleteWarning, getWarning } from 'src/modules/warnings/api/api';
import { ItemWarningDto } from 'src/models/weather/warningsDto';
import { ItemResponse } from 'src/models/common/itemResponse';

const useView = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: result , isLoading, isError, error } = useQuery<ItemResponse<ItemWarningDto>, Error>(
    ['warning', id],
    () => getWarning(id!),
    { enabled: !!id }
  );

  const deleteMutation = useMutation(deleteWarning, {
    onSuccess: (result) => {
      toast.success(result.data);
      setTimeout(() => {
        navigate("/warnings");
      }, 2000);
    },
    onError: (error) => {
      toast.error("An error occurred while deleting the warning.");
      console.error("Error deleting warning:", error);
    },
  });

  const handleDelete = async () => {
    if (!id) return;
    deleteMutation.mutate(id);
  };

  if (isError) {
    toast.error(error?.message || 'Failed to fetch warning');
  }

  return {
    warning: result?.data,
    handleDelete,
    navigate,
    id,
    isLoading,
    isError,
  };
};

export default useView;
