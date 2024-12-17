export interface WarningDto {
  name: string
  description: string
};

export interface PickerWarningDto {
  id: string
  name: string
}

export interface ListItemWarningDto {
  id: string
  name: string
  description: string
  count: number
}

export interface ItemWarningDto {
  id: string;
  name: string;
  description: string;
  forecasts: string[];
}
