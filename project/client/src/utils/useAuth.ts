// packages
import { useQuery } from "react-query";
import { useState } from "react";

interface User {
  id: string;
  email: string;
}

const parseJwt = (token: string): Record<string, any> | null => {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => `%${`00${c.charCodeAt(0).toString(16)}`.slice(-2)}`)
        .join("")
    );
    return JSON.parse(jsonPayload);
  } catch (error) {
    console.error("Invalid token:", error);
    return null;
  }
};

// Custom hook to fetch the token from localStorage and parse it
const useAuth = () => {
  const [roles, setRoles] = useState<string[]>([]);
  const [user, setUser] = useState<User | null>(null);

  // Fetch token from localStorage
  const { data: token, isLoading, isError } = useQuery(
    "authToken",
    () => {
      const token = localStorage.getItem("authToken");
      if (!token) {
        throw new Error("No token found");
      }
      return token;
    },
    {
      enabled: true, // Ensure it runs only when needed
      retry: false, // Don't retry fetching token
      onSuccess: (token) => {
        const decodedToken = parseJwt(token);
        if (decodedToken) {
          // Extract the roles from the decoded JWT
          const userRoles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
          setRoles(Array.isArray(userRoles) ? userRoles : [userRoles]);

          // Set user data
          setUser({
            id: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            email: decodedToken.sub,
          });
        }
      },
    }
  );

  // Role check functions
  const isAdministrator = () => roles.includes("Administrator");
  const isManager = () => roles.includes("Manager");
  const isUser = () => roles.includes("User");

  return { roles, user, isLoading, isError, isAdministrator, isManager, isUser };
};

export default useAuth;
