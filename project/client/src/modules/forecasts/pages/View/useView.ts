// packages
import { useQuery, useMutation } from "react-query";
import { toast } from "react-toastify";
import { useNavigate, useParams } from "react-router-dom";

// source
import { deleteForecast, getForecast } from "src/modules/forecasts/api/api";

const useView = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: result, isLoading, isError, error } = useQuery(
    ['forecast', id],
    () => getForecast(id!),
    {
      enabled: !!id,
      onError: (err) => {
        console.error("Error fetching forecast:", err);
        toast.error("Failed to fetch forecast.");
      }
    }
  );

  const deleteMutation = useMutation(deleteForecast, {
    onSuccess: (result) => {
      toast.success(result.data);
      setTimeout(() => {
        navigate("/forecasts");
      }, 2000);
    },
    onError: (error) => {
      console.error("Error deleting forecast:", error);
      toast.error("An error occurred while deleting the forecast.");
    }
  });

  const handleDelete = () => {
    if (!id) return;
    deleteMutation.mutate(id);
  };

  return {
    forecast: result?.data,
    isLoading,
    isError,
    error,
    handleDelete,
    navigate,
    id: id || "",
  };
};

export default useView;
