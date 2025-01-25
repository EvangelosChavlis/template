/**
 * Represents a minimal representation of a user with basic information.
 * 
 * This interface is used for listing users, including their name, email, username, and phone numbers.
 */
export interface ListItemUserDto {
    /**
     * The unique identifier for the user.
     */
    id: string;

    /**
     * The user's first name.
     */
    firstName: string;

    /**
     * The user's last name.
     */
    lastName: string;

    /**
     * The user's email address.
     */
    email: string;

    /**
     * The user's username.
     */
    userName: string;

    /**
     * The user's phone number.
     */
    phoneNumber: string;

    /**
     * The user's mobile phone number.
     */
    mobilePhoneNumber: string;
}

/**
 * Represents detailed information about a user, including personal and contact information, as well as account status.
 * 
 * This interface is used for full user data, including security details such as lockout status and roles.
 */
export interface ItemUserDto {
    /**
     * The unique identifier for the user.
     */
    id: string;
  
    /**
     * The user's first name.
     */
    firstName: string;
  
    /**
     * The user's last name.
     */
    lastName: string;
  
    /**
     * The user's email address.
     */
    email: string;
  
    /**
     * Whether the user's email has been confirmed.
     */
    emailConfirmed: boolean;
  
    /**
     * The user's username.
     */
    userName: string;
  
    /**
     * Whether the user is locked out of their account.
     */
    lockoutEnabled: boolean;
  
    /**
     * The date and time the user's lockout ends, if applicable.
     */
    lockoutEnd: string;
  
    /**
     * The user's initial password (used for user creation).
     */
    initialPassword: string;
  
    /**
     * The user's home address.
     */
    address: string;
  
    /**
     * The user's zip code.
     */
    zipCode: string;
  
    /**
     * The user's city.
     */
    city: string;
  
    /**
     * The user's state.
     */
    state: string;
  
    /**
     * The user's country.
     */
    country: string;
  
    /**
     * The user's phone number.
     */
    phoneNumber: string;
  
    /**
     * Whether the user's phone number is confirmed.
     */
    phoneNumberConfirmed: boolean;
  
    /**
     * The user's mobile phone number.
     */
    mobilePhoneNumber: string;
  
    /**
     * Whether the user's mobile phone number is confirmed.
     */
    mobilePhoneNumberConfirmed: boolean;
  
    /**
     * A short biography or description of the user.
     */
    bio: string;
  
    /**
     * The user's date of birth.
     */
    dateOfBirth: string;
  
    /**
     * Whether the user's account is active.
     */
    isActive: boolean;
}
  
/**
 * Represents the data used to create or update a user account.
 * 
 * This interface is used for user registration or profile creation, including all relevant personal information.
 */
export interface UserDto {
    /**
     * The user's first name.
     */
    firstName: string;
  
    /**
     * The user's last name.
     */
    lastName: string;
  
    /**
     * The user's email address.
     */
    email: string;
  
    /**
     * The user's username.
     */
    userName: string;
  
    /**
     * The user's password.
     */
    password: string;
  
    /**
     * The user's home address.
     */
    address: string;
  
    /**
     * The user's zip code.
     */
    zipCode: string;
  
    /**
     * The user's city.
     */
    city: string;
  
    /**
     * The user's state.
     */
    state: string;
  
    /**
     * The user's country.
     */
    country: string;
  
    /**
     * The user's phone number.
     */
    phoneNumber: string;
  
    /**
     * The user's mobile phone number.
     */
    mobilePhoneNumber: string;
  
    /**
     * A short biography or description of the user.
     */
    bio: string;
  
    /**
     * The user's date of birth.
     */
    dateOfBirth: Date;
}
  
/**
 * Represents the data used by a user to log in.
 * 
 * This interface includes the user's credentials (username and password).
 */
export interface UserLoginDto {
    /**
     * The user's username.
     */
    username: string;
  
    /**
     * The user's password.
     */
    password: string;
  }
  
/**
 * Represents a successfully authenticated user.
 * 
 * This interface contains the username and a token used for authentication in future requests.
 */
export interface AuthenticatedUserDto {
    /**
     * The username of the authenticated user.
     */
    userName: string;
  
    /**
     * A token used to authenticate the user in future requests.
     */
    token: string;
}
  
  /**
   * Represents the data used to request a password reset.
   * 
   * This interface includes the user's email address.
   */
  export interface ForgotPasswordDto {
    /**
     * The email address of the user requesting the password reset.
     */
    email: string;
  }
  
/**
 * Represents the data used to reset a password.
 * 
 * This interface includes the user's email, the reset token, and the new password.
 */
export interface ResetPasswordDto {
    /**
     * The email address of the user resetting the password.
     */
    email: string;
  
    /**
     * The token provided for the password reset process.
     */
    token: string;
  
    /**
     * The new password for the user.
     */
    newPassword: string;
}
  
/**
 * Represents the data used to enable Two-Factor Authentication (2FA) for a user.
 * 
 * This interface contains the user's ID.
 */
export interface Enable2FADto {
    /**
     * The unique identifier of the user enabling 2FA.
     */
    userId: string;
}
  
/**
 * Represents the data used to verify Two-Factor Authentication (2FA) for a user.
 * 
 * This interface includes the user's ID and the verification token.
 */
export interface Verify2FADto {
    /**
     * The unique identifier of the user verifying 2FA.
     */
    userId: string;
  
    /**
     * The token used to verify the 2FA process.
     */
    token: string;
}  