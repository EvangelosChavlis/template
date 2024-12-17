// packages
import * as yup from "yup";


const warningSchema = yup.object().shape({
    name: yup
        .string()
        .required("Name is required")
        .max(100, "Name must not exceed 100 characters."),
    description: yup
        .string()
        .required("Description is required")
        .max(500, "Description must not exceed 500 characters."),
});

export default warningSchema;