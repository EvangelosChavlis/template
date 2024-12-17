// packages
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useQuery } from "react-query";
import { toast } from "react-toastify";
import * as yup from "yup";

// source
import { ForecastDto } from "src/models/weather/forecastsDto";
import { initializeForecasts } from "src/modules/forecasts/api/api";
import forecastSchema from "src/modules/forecasts/validation/validationSchema";
import { getWarningsPicker } from "src/modules/warnings/api/api";
import { ItemResponse } from "src/models/common/itemResponse";
import { PickerWarningDto } from "src/models/weather/warningsDto";


const useInitialize = () => {
  const navigate = useNavigate();
  const [formDataList, setFormDataList] = useState<ForecastDto[]>([]); // Warnings list
  const [currentForecast, setCurrentForecast] = useState<ForecastDto>({
    date: "",
    temperatureC: 0,
    summary: "",
    warningId: ""
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setCurrentForecast({
      ...currentForecast,
      [name as keyof ForecastDto]: value,
    });
  };

  const addForm = async () => {
    try {
      // Validate currentWarning against schema
      await forecastSchema.validate(currentForecast, { abortEarly: false });
      setFormDataList([...formDataList, currentForecast]);
      setCurrentForecast({ 
        date: "",
        temperatureC: 0,
        summary: "",
        warningId: "" 
      });
      toast.success(`Forecast "${currentForecast.date}" added successfully!`);
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

  const submitForecasts = async () => {
    try {
      if (formDataList.length === 0) {
        toast.warning("No forecasts to submit.");
        return;
      }
      const result = await initializeForecasts(formDataList);
      toast.success(result.data);
      setTimeout(() => {
        navigate("/forecasts");
      }, 2000);
    } catch (error: any) {
      console.error("Error submitting forecasts:", error);
      toast.error(error.message || "An error occurred while submitting forecasts.");
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
    formDataList, 
    currentForecast, 
    handleChange, 
    addForm, 
    removeForm, 
    submitForecasts,
    warningsPicker 
  };
};

export default useInitialize;
