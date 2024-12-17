export interface ListItemTelemetryDto {
    id: string;
    method: string;
    path: string;
    statusCode: string;
    responseTime: number;
    requestTimestamp: string;
}
  
export interface ItemTelemetryDto {
    id: string; 
    method: string;
    path: string;
    statusCode: string;
    responseTime: number;
    memoryUsed: number;
    cpUusage: number;
    requestBodySize: number;
    requestTimestamp: string;
    responseBodySize: number;
    responseTimestamp: string;
    clientIp: string;
    userAgent: string;
    threadId: string;
}
  