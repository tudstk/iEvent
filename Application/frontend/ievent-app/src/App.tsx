import React, { useEffect, useState } from "react";
import { Routes, Route, useNavigate } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Home from "./pages/Home";
import ProtectedRoute from "./pages/ProtectedRoute/ProtectedRoute";
import Splash from "./pages/Splash";
import { verifyToken } from "./api/account";
import { TokenRequest } from "./types/requests/account/tokenRequest";

const App: React.FC = () => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const navigate = useNavigate();

  useEffect(() => {
    const checkAuthenticated = async () => {
      const token = localStorage.getItem("token");
      if (token) {
        try {
          const tokenRequest: TokenRequest = {
            token: token.toString()
          };

          const isValid = await verifyToken(tokenRequest);

          if (isValid)
          {
            setIsAuthenticated(true);
          }
          else
          {
            localStorage.removeItem("token");
          }
        } catch {
          localStorage.removeItem("token");
        }
      }
    };

    checkAuthenticated();
  }, []);

  useEffect(() => {
    if (isAuthenticated){
      navigate("/home");
    }
    else{
      navigate("/");
    }
  }, [isAuthenticated]);  

  return (
    <Routes>
      <Route path="/" element={<Splash />} />
      <Route path="/login" element={<Login setAuth={setIsAuthenticated} />} />
      <Route
        path="/register"
        element={<Register setAuth={setIsAuthenticated} />}
      />
      <Route
        path="/home"
        element={
          <ProtectedRoute isAuthenticated={isAuthenticated}>
            <Home />
          </ProtectedRoute>
        }
      />{" "}
    </Routes>
  );
};

export default App;
