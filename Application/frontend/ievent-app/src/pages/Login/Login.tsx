import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../api/account";
import { LoginRequest } from "../../types/requests/account/loginRequest";
import "./Login.css";

interface LoginProps {
  setAuth: (value: boolean) => void;
}

function Login({ setAuth }: LoginProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginRequest>();
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState("");

  const onSubmit = async (data: LoginRequest) => {
    const success = await login(data);

    if (success) {
      setAuth(true);
      navigate("/home");
    } else {
      setErrorMessage("Login failed. Please check your credentials.");
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h2 className="login-title">Login</h2>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
        <form className="login-form" onSubmit={handleSubmit(onSubmit)}>
          <div className="form-group">
            <label className="form-label">Username</label>
            <input
              className="form-input"
              type="text"
              {...register("username", { required: "Username is required." })}
            />
            {errors.username && (
              <p className="form-error">{errors.username.message as string}</p>
            )}
          </div>
          <div className="form-group">
            <label className="form-label">Password</label>
            <input
              className="form-input"
              type="password"
              {...register("password", {
                required: "Password is required.",
                minLength: {
                  value: 5,
                  message: "Password must be at least 5 characters long.",
                },
              })}
            />
            {errors.password && (
              <p className="form-error">{errors.password.message as string}</p>
            )}
          </div>
          <button className="login-button" type="submit">
            Login
          </button>
        </form>
        <p className="register-link">
          Don't have an account? <Link to="/register">Register</Link>
        </p>
      </div>
    </div>
  );
}

export default Login;
