import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Home.css";
import { getMyEvents, getRecommendedEvents, addEventToMyList, removeEventFromMyList } from "../../api/homereq";
import { getProfile } from "../../api/profileReq";
import { UserEventDto } from "../../types/dtos/home";

const Home: React.FC = () => {
  const [username, setUsername] = useState<string>("");
  const [myEvents, setMyEvents] = useState<UserEventDto[]>([]);
  const [recommendedEvents, setRecommendedEvents] = useState<UserEventDto[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const profile = await getProfile();
        setUsername(profile.userName);

        const [events, recommendations] = await Promise.all([
          getMyEvents(),
          getRecommendedEvents(),
        ]);

        setMyEvents(events);
        setRecommendedEvents(recommendations);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/home");
  };

  const handleAddEvent = async (event: UserEventDto) => {
    try {
      await addEventToMyList(event.id);
      setMyEvents((prev) => [...prev, event]);
      setRecommendedEvents((prev) => prev.filter((e) => e.id !== event.id));
    } catch (error) {
      console.error("Error adding event:", error);
    }
  };

  const handleRemoveEvent = async (event: UserEventDto) => {
    try {
      await removeEventFromMyList(event.id);
      setRecommendedEvents((prev) => [...prev, event]);
      setMyEvents((prev) => prev.filter((e) => e.id !== event.id));
    } catch (error) {
      console.error("Error removing event:", error);
    }
  };

  return (
    <div className="home-page">
      <header className="home-header">
        <h1 className="home-title">Welcome, {username}</h1>
        <div className="home-buttons">
          <button className="btn btn-logout" onClick={handleLogout}>
            Logout
          </button>
          <button className="btn btn-profile" onClick={() => navigate("/profile")}>
            Profile
          </button>
          <button className="btn btn-admin-panel" onClick={() => navigate("/admin-panel")}>
            Admin Panel
          </button>
        </div>
      </header>

      <div className="home-lists">
        <div className="event-list my-events">
          <h2 className="list-title">My Events</h2>
          <ul className="event-items">
            {myEvents.map((event) => (
              <li key={event.id} className="event-item" onClick={() => navigate(`/event/${event.id}`)}>
                <div className="event-details">
                  <h3 className="event-name">{event.name}</h3>
                  <p className="event-description">{event.description}</p>
                </div>
                <button
                  className="btn btn-remove"
                  onClick={(e) => {
                    e.stopPropagation();
                    handleRemoveEvent(event);
                  }}
                >
                  X
                </button>
              </li>
            ))}
          </ul>
        </div>

        <div className="event-list recommended-events">
          <h2 className="list-title">Recommended Events</h2>
          <ul className="event-items">
            {recommendedEvents.map((event) => (
              <li key={event.id} className="event-item" onClick={() => navigate(`/event/${event.id}`)}>
                <div className="event-details">
                  <h3 className="event-name">{event.name}</h3>
                  <p className="event-description">{event.description}</p>
                </div>
                <button
                  className="btn btn-heart"
                  onClick={(e) => {
                    e.stopPropagation();
                    handleAddEvent(event);
                  }}
                >
                  ❤️
                </button>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Home;
