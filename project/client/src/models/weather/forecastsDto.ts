export interface ListItemForecastDto {
    id: string;
    date: string;
    temperatureC: number;
    warning: string;
}

export interface ItemForecastDto {
    id: string;
    date: string;
    temperatureC: number;
    summary: string;
    warning: string;
}

export interface ForecastDto {
    date: string;
    temperatureC: number;
    summary: string;
    warningId: string;
}
