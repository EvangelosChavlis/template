// packages
import { useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useQuery } from "react-query";
import { useNavigate, useParams } from "react-router-dom";
import * as yup from "yup";

// source
import { ItemResponse } from "src/models/common/itemResponse";
import { getWarningsPicker } from "src/modules/warnings/api/api";
import { ItemForecastDto } from "src/models/weather/forecastsDto";
import { getForecast, updateForecast } from "src/modules/forecasts/api/api";
import forecastSchema from "src/modules/forecasts/validation/validationSchema";
import { PickerWarningDto } from "src/models/weather/warningsDto";

interface FormValues {
  preventDefault: () => void;
  date: string;
  temperatureC: number;
  summary: string;
  warningId: string;
}

const useUpdate = () => {
  const [form, setForm] = useState({
    date: "",
    temperatureC: 0,
    summary: "",
    warningId: ""
  });

  const [errors, setErrors] = useState<{ 
    date?: string;
    temperatureC?: number;
    summary?: string;
    warningId?: string;
  }>({});

  const [isValid, setIsValid] = useState<{ 
    date: boolean; 
    temperatureC: boolean;
    summary: boolean;
    warningId: boolean;
  }>({
    date: true,
    temperatureC: true,
    summary: true,
    warningId: true
  });

  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data: result } = useQuery<ItemResponse<ItemForecastDto>, Error>(
    ["forecast", id],
    () => getForecast(id!),
    { enabled: !!id }
  );

  const { data: warningsPicker} = useQuery<ItemResponse<PickerWarningDto[]>, Error>(
    "warnings-picker",
    getWarningsPicker,
    {
      onError: (error) => {
        console.error("Error fetching warnings picker:", error);
      },
    }
  );

  useMemo(() => {
    if (result) {
      setForm({
        date: result.data!.date,
        temperatureC: result.data!.temperatureC,
        summary: result.data!.summary,
        warningId: result.data!.warning
      });
    }
  }, [result]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setForm((prevForm) => ({
      ...prevForm,
      [name]: value,
    }));
  };

  const handleSubmit = async ({ 
    preventDefault, 
    date,
    temperatureC, 
    summary,
    warningId
  }: FormValues) => {
    preventDefault();

    try {
      // Validate the form using the same schema as in the create function
      await forecastSchema.validate({ date, temperatureC, summary, warningId }, { abortEarly: false });

      // Send the update request if validation passes
      const result = await updateForecast(id!, { date, temperatureC, summary, warningId });
      toast.success(result.data);
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

        // Update error state and validity status
        setErrors(fieldErrors);
        setIsValid({
          date: !fieldErrors.date,
          temperatureC: !fieldErrors.temperatureC,
          summary: !fieldErrors.summary,
          warningId: !fieldErrors.warningId,
        });
      } else {
        console.error("Error updating forecast:", error);
        toast.error("An error occurred while updating the forecast.");
      }
    }
  };

  return {
    form,
    errors,
    isValid,
    handleChange,
    handleSubmit,
    warningsPicker
  };
};

export default useUpdate;
