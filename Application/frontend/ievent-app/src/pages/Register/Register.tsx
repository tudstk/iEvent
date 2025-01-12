import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { RegisterRequest } from "../../types/requests/account/registerRequest";
import { register } from "../../api/account";
import "./Register.css";

interface RegisterProps {
  setAuth: (value: boolean) => void;
}

function Register({ setAuth }: RegisterProps) {
  const {
    register: formRegister,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterRequest>();
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState("");

  const onSubmit = async (data: RegisterRequest) => {
    const success = await register(data);

    if (success) {
      setAuth(true);
      navigate("/home");
    } else {
      setErrorMessage("Registration failed. Please try again.");
    }
  };

  return (
    <div className="register-page">
      <div className="register-container">
        <h2 className="register-title">Register</h2>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
        <form className="register-form" onSubmit={handleSubmit(onSubmit)}>
          <div className="form-group">
            <label className="form-label">Username</label>
            <input
              className="form-input"
              type="text"
              {...formRegister("username", { required: "Username is required." })}
            />
            {errors.username && (
              <p className="form-error">{errors.username.message as string}</p>
            )}
          </div>
          <div className="form-group">
            <label className="form-label">Email</label>
            <input
              className="form-input"
              type="email"
              {...formRegister("email", {
                required: "Email is required.",
                pattern: {
                  value: /^[^@ ]+@[^@ ]+\.[^@ .]{2,}$/,
                  message: "Invalid email format.",
                },
              })}
            />
            {errors.email && (
              <p className="form-error">{errors.email.message as string}</p>
            )}
          </div>
          <div className="form-group">
            <label className="form-label">Password</label>
            <input
              className="form-input"
              type="password"
              {...formRegister("password", {
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
          <button className="register-button" type="submit">
            Register
          </button>
        </form>
        <p className="login-link">
          Already have an account? <Link to="/">Login</Link>
        </p>
      </div>
    </div>
  );
}

export default Register;
