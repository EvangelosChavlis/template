// packages
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import * as yup from "yup";

// source
import { createRole } from "src/modules/roles/api/api";
import roleSchema from "src/modules/roles/validation/validationSchema";

interface FormValues {
  preventDefault: () => void;
  name: string;
  description: string;
}

const useCreate = () => {
  const navigate = useNavigate();

  const handleSubmit = async ({ preventDefault, name, description }: FormValues) => {
    preventDefault();
  
    try {
      await roleSchema.validate({ name, description }, { abortEarly: false });
  
      const result = await createRole({ name, description });
      toast.success(result.data);
      setTimeout(() => {
        navigate("/roles");
      }, 2000);
    } catch (error) {
      if (error instanceof yup.ValidationError) {
        // Collect errors in a field-specific format
        const fieldErrors = error.inner.reduce((acc, curr) => {
          if (curr.path) acc[curr.path] = curr.message;
          return acc;
        }, {} as { [key: string]: string });
  
        throw fieldErrors; // Propagate errors to the caller
      } else {
        console.error("Error creating role:", error);
        toast.error("An error occurred while creating the role.");
      }
    }
  };
  
  return {
    handleSubmit,
  };
};

export default useCreate;
