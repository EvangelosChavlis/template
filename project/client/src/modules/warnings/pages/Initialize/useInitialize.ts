// packages
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import * as yup from "yup";

// source
import warningSchema from "src/modules/warnings/validation/validationSchema";
import { WarningDto } from "src/models/weather/warningsDto";
import { initializeWarnings } from "src/modules/warnings/api/api";


const useInitialize = () => {
  const navigate = useNavigate();
  const [formDataList, setFormDataList] = useState<WarningDto[]>([]); // Warnings list
  const [currentWarning, setCurrentWarning] = useState<WarningDto>({
    name: "",
    description: "",
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setCurrentWarning({
      ...currentWarning,
      [name as keyof WarningDto]: value,
    });
  };

  const addForm = async () => {
    try {
      // Validate currentWarning against schema
      await warningSchema.validate(currentWarning, { abortEarly: false });
      setFormDataList([...formDataList, currentWarning]);
      setCurrentWarning({ name: "", description: "" });
      toast.success(`Warning "${currentWarning.name}" added successfully!`);
    } catch (error) {
      if (error instanceof yup.ValidationError) {
        // Display error messages
        error.errors.forEach((err) => toast.error(err));
      }
    }
  };

  const removeForm = (index: number) => {
    const newList = [...formDataList];
    newList.splice(index, 1);
    setFormDataList(newList);
    toast.info("Warning removed.");
  };

  const submitWarnings = async () => {
    try {
      if (formDataList.length === 0) {
        toast.warning("No warnings to submit.");
        return;
      }
      const result = await initializeWarnings(formDataList);
      toast.success(result.data);
      setTimeout(() => {
        navigate("/warnings");
      }, 2000);
    } catch (error: any) {
      console.error("Error submitting warnings:", error);
      toast.error(error.message || "An error occurred while submitting warnings.");
    }
  };

  return { formDataList, currentWarning, handleChange, addForm, removeForm, submitWarnings };
};

export default useInitialize;
