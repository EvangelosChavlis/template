import { Pagination } from './pagination'

export interface ListResponse<T> {
    data?: T
    pagination: Pagination
  }

export function withData<T>(response: ListResponse<T>, data: T): ListResponse<T> {
    response.data = data;
    return response;
}