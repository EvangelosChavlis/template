export interface ItemResponse<T> {
    data?: T;
  }
  
export function withData<T>(response: ItemResponse<T>, data: T): ItemResponse<T> {
  response.data = data;
  return response;
}
  