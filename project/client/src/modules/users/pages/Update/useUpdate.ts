// packages
import { toast } from "react-toastify";
import { useNavigate, useParams } from "react-router-dom";
import * as yup from "yup";
import { useQuery } from "react-query";

// source
import { updateUser, getUser } from "src/modules/users/api/api";
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

const useUpdate = () => {
  const { id } = useParams<{ id: string }>();
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

  
      const result = await updateUser(id!, { 
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
        console.error("Error updating user:", error);
        toast.error("An error occurred while updating the user.");
      }
    }
  };

  const { data: user } = useQuery(
          ['user', id],
          () => getUser(id!),
          {
              enabled: !!id,
              suspense: true,
              onError: (err: any) => {
                  console.error("Error fetching user:", err);
                  toast.error("Failed to fetch user.");
              },
          }
      );

  return {
    user: user?.data!,
    handleSubmit
  };
};

export default useUpdate;
