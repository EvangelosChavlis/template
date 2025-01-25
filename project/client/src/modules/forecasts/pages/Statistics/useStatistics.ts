// packages
import { useQuery } from "react-query";

// source
import { ItemResponse } from "src/models/common/itemResponse";
import { StatItemForecastDto } from "src/models/weather/forecastsDto";
import { getForecastsStats } from "src/modules/forecasts/api/api";

const useStatistics = () => {
  const { data: forecasts, isLoading, isError, error } = useQuery<ItemResponse<StatItemForecastDto[]>, Error>(
    "forecasts-stats",
    getForecastsStats,
    {
      suspense: true,
      onError: (error) => {
        console.error("Error fetching forecasts stasts:", error);
      },
    }
  );

  return {
    forecasts: forecasts || { data: [] },
    isLoading,
    isError,
    error,
  };
};

export default useStatistics;
