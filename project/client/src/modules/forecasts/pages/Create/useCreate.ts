// packages
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import * as yup from "yup";

// source
import { createForecast } from "src/modules/forecasts/api/api";
import forecastSchema from "src/modules/forecasts/validation/validationSchema";
import { PickerWarningDto } from "src/models/weather/warningsDto";
import { useQuery } from "react-query";
import { getWarningsPicker } from "src/modules/warnings/api/api";
import { ItemResponse } from "src/models/common/itemResponse";

// Define the FormValues interface based on ForecastDto
interface FormValues {
  preventDefault: () => void;
  date: string;
  temperatureC: number;
  summary: string;
  warningId: string;
}

const useCreate = () => {
  const navigate = useNavigate();

  const handleSubmit = async ({ preventDefault, date, temperatureC, summary, warningId }: FormValues) => {
    preventDefault();

    try {
      // Validate the input against the forecast schema
      await forecastSchema.validate({ date, temperatureC, summary, warningId }, { abortEarly: false });

      // Call the API to create the forecast
      const result = await createForecast({ date, temperatureC, summary, warningId });
      toast.success(result.data);

      // Redirect to the forecasts page after a delay
      setTimeout(() => {
        navigate("/forecasts");
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
        console.error("Error creating forecast:", error);
        toast.error("An error occurred while creating the forecast.");
      }
    }
  };

  const { data: warningsPicker} = useQuery<ItemResponse<PickerWarningDto[]>, Error>(
      "warnings-picker",
      getWarningsPicker,
      {
        onError: (error) => {
          console.error("Error fetching warnings picker:", error);
        },
      }
    );

  return {
    handleSubmit,
    warningsPicker
  };
};

export default useCreate;
