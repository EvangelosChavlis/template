/**
 * Represents a role used in the system.
 * 
 * This interface defines the basic properties of a role, including its name and description.
 */
export interface RoleDto {
  /**
   * The name of the role (e.g., "Admin", "User").
   */
  name: string;

  /**
   * A brief description of the role's purpose or responsibilities.
   */
  description: string;
}

/**
 * Represents detailed information about a specific role.
 * 
 * This interface extends the basic role properties by adding an ID and an "isActive" flag,
 * which indicates whether the role is currently active.
 */
export interface ItemRoleDto {
  /**
   * The unique identifier for the role.
   */
  id: string;

  /**
   * The name of the role (e.g., "Admin", "User").
   */
  name: string;

  /**
   * A description of the role's purpose or responsibilities.
   */
  description: string;

  /**
   * Indicates whether the role is currently active (true) or inactive (false).
   */
  isActive: boolean;
}