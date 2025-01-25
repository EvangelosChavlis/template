// packages
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import * as yup from "yup";

// source
import userValidationSchema from "src/modules/users/validation/validationSchema";
import { register } from "src/modules/auth/api/api";

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

const useRegister = () => {
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

  
      const result = await register({ 
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
        navigate("/auth/login");
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
        console.error("Error in register user:", error);
        toast.error("An error occurred in register the user.");
      }
    }
  };

  return {
    handleSubmit
  };
};

export default useRegister;
