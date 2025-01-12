import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./AdminPanel.css";
import { extractJwtPayload } from "../../utils/jwtUtils";
import ArtistsCrud from "./ArtistsCrud";
import Sidebar from "./SideBar";
import LocationsCrud from "./LocationsCrud";
import GenresCrud from "./GenresCrud";
import EventTypesCrud from "./EventTypesCrud";
import EventsCrud from "./EventsCrud";

const AdminPanel: React.FC = () => {
  const [selectedComponent, setSelectedComponent] = useState<string>("Events");
  const [isAdmin, setIsAdmin] = useState(false);
  const [isOrganizer, setIsOrganizer] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    const tokenInfo = extractJwtPayload(token || "");
    const isAdminInfo = tokenInfo?.isAdmin === "true";
    const isOrganizerInfo = tokenInfo?.isOrganizer === "true";

    if (!isAdminInfo && !isOrganizerInfo) {
      console.log("User is not authorized to access this page");
      navigate("/home"); 
    }
    else{
      setIsAdmin(tokenInfo?.isAdmin === "true");
      setIsOrganizer(tokenInfo?.isOrganizer === "true");
    } 

  }, []);

  const renderComponent = () => {
    switch (selectedComponent) {
      case "Artists":
        return <ArtistsCrud />;
      case "Events":
        return <EventsCrud />;
      case "EventTypes":
        return <EventTypesCrud />;
      case "Genres":
        return <GenresCrud />;
      case "Locations":
        return <LocationsCrud />;
      default:
        return null;
    }
  };

  return (
    <div className="admin-panel">
      <Sidebar
        setSelectedComponent={setSelectedComponent}
        isAdmin={isAdmin}
        isOrganizer={isOrganizer}
      />
      <div className="content-area">
        <button className="home-button" onClick={() => navigate("/home")}>
          Home
        </button>
        {renderComponent()}
      </div>
    </div>
  );
};

export default AdminPanel;
