// packages
import { useState } from "react";
import { useNavigate } from "react-router-dom";

// source
import { login } from "src/modules/auth/api/api";

const UseLogin = () => {
  const [username, setUsername] = useState<string>("evangelos.chavlis");
  const [password, setPassword] = useState<string>("Ar@g0rn1996");
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    // Simple validation
    if (!username || !password) {
      setError("Both fields are required.");
      return;
    }

    try {
      setError(null);
   
      const response = await login({ username, password });
      
      if (response.success) {
        // Save token to localStorage
        localStorage.setItem("authToken", response.data?.token!);
        
        // You can also save user information if you need it for later use
        localStorage.setItem("userName", response.data?.userName!);

        // Navigate to the home or dashboard page after successful login
        navigate("/");
      } else {
        setError("Invalid credentials");
      }
    } catch (err: any) {
      setError(err.message || "An unexpected error occurred");
    }
  };

  return { username, password, error, setUsername, setPassword, handleLogin };
};

export default UseLogin;
