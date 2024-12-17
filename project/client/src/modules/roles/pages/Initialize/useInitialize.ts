// packages
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import * as yup from "yup";

// source
import { RoleDto } from "src/models/auth/roleDto";
import roleSchema from "src/modules/roles/validation/validationSchema";
import { initializeRoles } from "src/modules/roles/api/api"; // Import API function

const useInitialize = () => {
  const navigate = useNavigate();
  const [formDataList, setFormDataList] = useState<RoleDto[]>([]); // Roles list
  const [currentRole, setCurrentRole] = useState<RoleDto>({ name: "", description: "" }); // Current input

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setCurrentRole({
      ...currentRole,
      [name as keyof RoleDto]: value,
    });
  };

  const addForm = async () => {
    try {
      // Validate currentRole against schema
      await roleSchema.validate(currentRole, { abortEarly: false });
      setFormDataList([...formDataList, currentRole]);
      setCurrentRole({ name: "", description: "" });
      toast.success(`Role ${currentRole.name} added successfully!`);
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
    toast.info("Role removed.");
  };

  const submitRoles = async () => {
    try {
      if (formDataList.length === 0) {
        toast.warning("No roles to submit.");
        return;
      }
      const result = await initializeRoles(formDataList);
      toast.success(result.data);
      setTimeout(() => {
        navigate("/roles");
      }, 2000);
    } catch (error: any) {
      console.error("Error submitting roles:", error);
      toast.error(error.message || "An error occurred while submitting roles.");
    }
  };

  return { formDataList, currentRole, handleChange, addForm, removeForm, submitRoles };
};

export default useInitialize;
