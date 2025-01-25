/**
 * Represents a warning.
 * 
 * This interface includes the basic details of a warning, such as its name and description.
 */
export interface WarningDto {
  /**
   * The name of the warning (e.g., "Severe Storm").
   */
  name: string;

  /**
   * A detailed description of the warning.
   */
  description: string;
}

/**
 * Represents a warning in a picker context, typically used for selection.
 * 
 * This interface includes a simple identifier and the name of the warning.
 */
export interface PickerWarningDto {
  /**
   * The unique identifier for the warning.
   */
  id: string;

  /**
   * The name of the warning.
   */
  name: string;
}

/**
 * Represents a list item for a warning with additional metadata.
 * 
 * This interface includes details such as the warning's name, description, and how many times it's been applied (count).
 */
export interface ListItemWarningDto {
  /**
   * The unique identifier for the warning.
   */
  id: string;

  /**
   * The name of the warning (e.g., "Flood Warning").
   */
  name: string;

  /**
   * A detailed description of the warning.
   */
  description: string;

  /**
   * The number of times this warning has been issued or applied.
   */
  count: number;
}

/**
 * Represents detailed information about a single warning.
 * 
 * This interface includes the warning's name, description, and the list of forecast IDs associated with this warning.
 */
export interface ItemWarningDto {
  /**
   * The unique identifier for the warning.
   */
  id: string;

  /**
   * The name of the warning.
   */
  name: string;

  /**
   * A detailed description of the warning.
   */
  description: string;

  /**
   * A list of forecast IDs associated with this warning.
   */
  forecasts: string[];
}
