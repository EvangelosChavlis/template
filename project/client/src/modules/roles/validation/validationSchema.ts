// packages
import * as yup from "yup";

const roleSchema = yup.object().shape({
  name: yup
    .string()
    .required("Name is required.")
    .max(100, "Name must not exceed 100 characters."),
  description: yup
    .string()
    .required("Description is required.")
    .max(250, "Description must not exceed 250 characters."),
});

export default roleSchema;
