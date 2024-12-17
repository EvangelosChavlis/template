export interface ListItemUserDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    phoneNumber: string;
    mobilePhoneNumber: string;
}

export interface ItemUserDto {
    id: string;

    firstName: string;
    lastName: string;
    email: string;
    emailConfirmed: boolean;
    userName: string;
    lockoutEnabled: boolean;
    lockoutEnd: string;
    initialPassword: string;

    address: string;
    zipCode: string;
    city: string;
    state: string;
    country: string;
    phoneNumber: string;
    phoneNumberConfirmed: boolean;
    mobilePhoneNumber: string;
    mobilePhoneNumberConfirmed: boolean;

    bio: string;
    dateOfBirth: string;

    isActive: boolean;

    roles: string[];
}

export interface UserDto {
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    password: string;

    address: string;
    zipCode: string;
    city: string;
    state: string;
    country: string;
    phoneNumber: string;
    mobilePhoneNumber: string;

    bio: string;
    dateOfBirth: Date;
}

export interface UserLoginDto {
    username: string;
    password: string;
}

export interface AuthenticatedUserDto {
    userName: string;
    token: string;
}

export interface ForgotPasswordDto {
    email: string;
}

export interface ResetPasswordDto {
    email: string;
    token: string;
    newPassword: string;
}

export interface Enable2FADto {
    userId: string;
}

export interface Verify2FADto {
    userId: string;
    token: string;
}