/**
 * Represents the statistical data for a forecast item.
 * 
 * This interface includes essential statistics like temperature and the date it was recorded.
 */
export interface StatItemForecastDto {
    /**
     * The unique identifier for the forecast statistic.
     */
    id: string;

    /**
     * The date the forecast statistic corresponds to.
     */
    date: string;

    /**
     * The temperature in Celsius recorded for the forecast.
     */
    temperatureC: number;
}

/**
 * Represents a forecast item in a paginated list.
 * 
 * This interface includes basic details about the forecast, such as the date, temperature, and any associated warnings.
 */
export interface ListItemForecastDto {
    /**
     * The unique identifier for the forecast.
     */
    id: string;

    /**
     * The date of the forecast.
     */
    date: string;

    /**
     * The temperature in Celsius recorded for the forecast.
     */
    temperatureC: number;

    /**
     * A warning message associated with the forecast, if any.
     */
    warning: string;
}

/**
 * Represents detailed information about a single forecast item.
 * 
 * This interface includes more in-depth details such as the forecast summary and any associated warnings.
 */
export interface ItemForecastDto {
    /**
     * The unique identifier for the forecast.
     */
    id: string;

    /**
     * The date of the forecast.
     */
    date: string;

    /**
     * The temperature in Celsius recorded for the forecast.
     */
    temperatureC: number;

    /**
     * A summary of the forecast, providing additional context or description.
     */
    summary: string;

    /**
     * A warning message associated with the forecast, if any.
     */
    warning: string;
}

/**
 * Represents the data for creating or updating a forecast.
 * 
 * This interface includes the required details for creating a new forecast or updating an existing one, such as date, temperature, and warning.
 */
export interface ForecastDto {
    /**
     * The date for the forecast.
     */
    date: string;

    /**
     * The temperature in Celsius for the forecast.
     */
    temperatureC: number;

    /**
     * A summary of the forecast.
     */
    summary: string;

    /**
     * The unique identifier for the warning associated with the forecast.
     */
    warningId: string;
}
