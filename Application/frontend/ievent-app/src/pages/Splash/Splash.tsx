import React from "react";
import { useNavigate } from "react-router-dom";
import "./Splash.css";

const Splash: React.FC = () => {
  const navigate = useNavigate();

  return (
    <div className="splash-container">
      <h1 className="splash-title">Welcome to IEvent</h1>
      <div className="button-group">
        <button className="button-76" onClick={() => navigate("/login")}>
          Login
        </button>
        <button className="button-76" onClick={() => navigate("/register")}>
          Register
        </button>
      </div>
    </div>
  );
};

export default Splash;
