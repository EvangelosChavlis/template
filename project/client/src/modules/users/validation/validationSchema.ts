// packages
import * as yup from "yup";

const userValidationSchema = yup.object().shape({
  firstName: yup
    .string()
    .required("First name is required.")
    .max(50, "First name must not exceed 50 characters."),
  lastName: yup
    .string()
    .required("Last name is required.")
    .max(50, "Last name must not exceed 50 characters."),
  email: yup
    .string()
    .required("Email is required.")
    .email("Invalid email format."),
  userName: yup
    .string()
    .required("Username is required.")
    .max(50, "Username must not exceed 50 characters."),
  password: yup
    .string()
    .required("Password is required.")
    .min(8, "Password must be at least 8 characters.")
    .matches(/[A-Z]/, "Password must contain at least one uppercase letter.")
    .matches(/[a-z]/, "Password must contain at least one lowercase letter.")
    .matches(/\d/, "Password must contain at least one number.")
    .matches(/[\W]/, "Password must contain at least one special character."),
  address: yup
    .string()
    .required("Address is required."),
  zipCode: yup
    .string()
    .required("ZipCode is required.")
    .matches(/^\d{5}(-\d{4})?$/, "Invalid ZipCode format."),
  city: yup
    .string()
    .required("City is required."),
  state: yup
    .string()
    .required("State is required."),
  country: yup
    .string()
    .required("Country is required."),
  phoneNumber: yup
    .string()
    .required("Phone number is required.")
    .matches(/^\+?\d{10,15}$/, "Phone number must be a valid international format."),
  mobilePhoneNumber: yup
    .string()
    .required("Mobile phone number is required.")
    .matches(/^\+?\d{10,15}$/, "Mobile phone number must be a valid international format."),
  bio: yup
    .string()
    .max(500, "Bio must not exceed 500 characters."),
  dateOfBirth: yup
    .date()
    .required("Date of birth is required.")
    .max(new Date(Date.now() - 18 * 365 * 24 * 60 * 60 * 1000), "User must be at least 18 years old."),
});

export default userValidationSchema;
