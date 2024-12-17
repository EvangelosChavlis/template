export interface ListItemErrorDto {
    id: string;
    error: string;
    statusCode: number;
    timestamp: string;
}

export interface ItemErrorDto {
    id: string;
    error: string;
    statusCode: number;
    instance: string;
    exceptionType: string;
    stackTrace: string;
    timestamp: string;
}