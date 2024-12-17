// source
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { ItemErrorDto, ListItemErrorDto } from "src/models/metrics/errorsDto";
import { baseUrl, urlParams } from "src/utils/utils";

export const getErrors = async (pagination: Pagination): Promise<ListResponse<ListItemErrorDto[]>> => {
    const params = urlParams(pagination);
    const response = await fetch(`${baseUrl}/metrics/errors${params}`, {
        method: "GET",
    });

    if (!response.ok) {
        throw new Error("Failed to fetch errors");
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


export const getError = async (id: string): Promise<ItemResponse<ItemErrorDto>> => {
    const response = await fetch(`${baseUrl}/metrics/errors/${id}`);
    if (!response.ok) {
        throw new Error("Failed to fetch error");
    }

    const result = await response.json();
    return result;
};