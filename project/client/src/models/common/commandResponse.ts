export interface CommandResponse<T> {
    data?: T;
    success: boolean
  }

export function withData<T>(response: CommandResponse<T>, data: T): CommandResponse<T> {
    response.data = data;
    return response;
}

export function withSuccess<T>(response: CommandResponse<T>, success: boolean): CommandResponse<T> {
    response.success = success;
    return response;
}