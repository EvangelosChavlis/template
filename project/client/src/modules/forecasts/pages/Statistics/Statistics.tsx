// packages
import { useEffect, useRef } from "react";
import { Chart, registerables } from "chart.js";

// source
import useStatistics from "src/modules/forecasts/pages/Statistics/useStatistics";
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";

// Register Chart.js components
Chart.register(...registerables);

const Statistics = () => {
  const { forecasts, isLoading, isError } = useStatistics();
  const chartRef = useRef<Chart | null>(null);
  const canvasRef = useRef<HTMLCanvasElement | null>(null);

  useEffect(() => {
    if (isLoading || isError || !forecasts?.data?.length) return;

    // Extract labels and data
    const labels = forecasts.data.map((forecast) => forecast.date);
    const data = forecasts.data.map((forecast) => forecast.temperatureC);

    // Destroy existing chart instance if any
    if (chartRef.current) {
      chartRef.current.destroy();
    }

    // Create new chart instance
    if (canvasRef.current) {
      chartRef.current = new Chart(canvasRef.current, {
        type: "line",
        data: {
          labels,
          datasets: [
            {
              label: "Temperature (°C)",
              data,
              borderColor: "rgba(75, 192, 192, 1)",
              backgroundColor: "rgba(75, 192, 192, 0.2)",
              borderWidth: 1,
              tension: 0.4, // Add smooth curves
            },
          ],
        },
        options: {
          responsive: true,
          plugins: {
            legend: {
              display: true,
              position: "top",
            },
          },
          scales: {
            x: {
              title: {
                display: true,
                text: "Date",
              },
            },
            y: {
              title: {
                display: true,
                text: "Temperature (°C)",
              },
              min: -10,
              max: 40,
            },
          },
        },
      });
    }

    // Cleanup function to destroy the chart instance on unmount
    return () => {
      if (chartRef.current) {
        chartRef.current.destroy();
        chartRef.current = null;
      }
    };
  }, [forecasts, isLoading, isError]);

  const buttons: ButtonProps[] = [];

  const header = "Statistics Info";

  return (
    <div className="container mt-4">
    <Header header={header} buttons={buttons} />
    <div
        className="p-3 border rounded bg-light mt-2"
        style={{ flex: 1, overflowY: "auto" }}
      >
        {isLoading && <p>Loading...</p>}
        {isError && <p>Error loading data</p>}
        {!isLoading && !isError && forecasts.data?.length! > 0 && (
            <canvas ref={canvasRef} id="temperatureChart"/>
        )}
      </div>
    </div>
  );
};

export default Statistics;
