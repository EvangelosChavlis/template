// source
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { ItemTelemetryDto, ListItemTelemetryDto } from "src/models/metrics/telemetryDto";
import { baseUrl, urlParams } from "src/utils/utils";

export const getTelemetryList = async (pagination: Pagination): Promise<ListResponse<ListItemTelemetryDto[]>> => {
    const params = urlParams(pagination);
    const response = await fetch(`${baseUrl}/metrics/telemetry${params}`, {
        method: "GET",
    });

    if (!response.ok) {
        throw new Error("Failed to fetch telemetry list");
    }

    const result = await response.json();
    return {
        data: result.data,
        pagination: {
        totalRecords: result.pagination.totalRecords,
        totalPages: Math.ceil(result.pagination.totalRecords / pagination.pageSize),
        pageNumber: pagination.pageNumber,
        pageSize: pagination.pageSize,
        },
    };
};


export const getTelemetryItem = async (id: string): Promise<ItemResponse<ItemTelemetryDto>> => {
    const response = await fetch(`${baseUrl}/metrics/telemetry/${id}`);
    if (!response.ok) {
        throw new Error("Failed to fetch telemetry item");
    }

    const result = await response.json();
    return result;
};