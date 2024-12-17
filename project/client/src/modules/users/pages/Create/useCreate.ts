// packages
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import * as yup from "yup";

// source
import { createUser } from "src/modules/users/api/api";
import userValidationSchema from "src/modules/users/validation/validationSchema";

interface FormValues {
  preventDefault: () => void;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  password: string;

  address: string;
  zipCode: string;
  city: string;
  state: string;
  country: string;
  phoneNumber: string;
  mobilePhoneNumber: string;

  bio: string;
  dateOfBirth: Date;
}

const useCreate = () => {
  const navigate = useNavigate();

  const handleSubmit = async ({ preventDefault, 
    firstName, 
    lastName,
    email,
    userName,
    password,

    address,
    zipCode,
    city,
    state,
    country,
    phoneNumber,
    mobilePhoneNumber,

    bio,
    dateOfBirth
  }: FormValues) => {
    preventDefault();

  
    try {
      await userValidationSchema.validate({ 
        firstName, 
        lastName,
        email,
        userName,
        password,
        
        address,
        zipCode,
        city,
        state,
        country,
        phoneNumber,
        mobilePhoneNumber,

        bio,
        dateOfBirth
       }, { abortEarly: false });

  
      const result = await createUser({ 
        firstName, 
        lastName,
        email,
        userName,
        password,
        
        address,
        zipCode,
        city,
        state,
        country,
        phoneNumber,
        mobilePhoneNumber,

        bio,
        dateOfBirth });
      toast.success(result.data);
      setTimeout(() => {
        navigate("/users");
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
        console.error("Error creating user:", error);
        toast.error("An error occurred while creating the user.");
      }
    }
  };

  return {
    handleSubmit
  };
};

export default useCreate;
