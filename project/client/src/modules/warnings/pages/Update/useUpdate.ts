// packages
import { useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useQuery } from "react-query";
import { useNavigate, useParams } from "react-router-dom";
import * as yup from "yup";

// source
import roleSchema from "src/modules/roles/validation/validationSchema";
import { ItemResponse } from "src/models/common/itemResponse";
import { getWarning, updateWarning } from "src/modules/warnings/api/api";
import { ItemWarningDto } from "src/models/weather/warningsDto";

interface FormValues {
  preventDefault: () => void;
  name: string;
  description: string;
}

const useUpdate = () => {
  const [form, setForm] = useState({
    name: "",
    description: "",
  });
  const [errors, setErrors] = useState<{ name?: string; description?: string }>({});
  const [isValid, setIsValid] = useState<{ name: boolean; description: boolean }>({
    name: true,
    description: true,
  });
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: result } = useQuery<ItemResponse<ItemWarningDto>, Error>(
    ["warning", id],
    () => getWarning(id!),
    {
      suspense: true, 
      enabled: !!id 
    }
  );

  useMemo(() => {
    if (result) {
      setForm({
        name: result.data!.name,
        description: result.data!.description,
      });
    }
  }, [result]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setForm((prevForm) => ({
      ...prevForm,
      [name]: value,
    }));
  };

  const handleSubmit = async ({ preventDefault, name, description }: FormValues) => {
    preventDefault();

    try {
      // Validate the form using the same schema as in the create function
      await roleSchema.validate({ name, description }, { abortEarly: false });

      // Send the update request if validation passes
      const result = await updateWarning(id!, { name, description });
      toast.success(result.data);
      setTimeout(() => {
        navigate("/warnings");
      }, 2000);
    } catch (error) {
      if (error instanceof yup.ValidationError) {
        // Collect errors in a field-specific format
        const fieldErrors = error.inner.reduce((acc, curr) => {
          if (curr.path) acc[curr.path] = curr.message;
          return acc;
        }, {} as { [key: string]: string });

        // Update error state and validity status
        setErrors(fieldErrors);
        setIsValid({
          name: !fieldErrors.name,
          description: !fieldErrors.description,
        });
      } else {
        console.error("Error updating warning:", error);
        toast.error("An error occurred while updating the warning.");
      }
    }
  };

  return {
    form,
    errors,
    isValid,
    handleChange,
    handleSubmit,
  };
};

export default useUpdate;
